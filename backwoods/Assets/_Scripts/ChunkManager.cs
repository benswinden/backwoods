using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ChunkManager : MonoBehaviour {

    public int totalChunks;
    public string firstChunk;

    public List<Chunk> originalChunkList = new List<Chunk>();        // List of all original chunks

    Chunk lastChunk;


    public Chunk startChunk { get; set; }                  // The chunk the player starts on, to be destroyed after a set period of time TODO: Messy
    public Chunk activeChunk { get; set; }                 // The reference to the original chunk currently being looped
    public Chunk newestChunk { get; set; }                 // Reference to the most recent chunk created to actually be used

    public Chunk currentChunk { get; set; }                // The instantiated chunk the player is currently riding on


    List<GameObject> chunkList = new List<GameObject>();

    void Awake() {

        Manager.chunkManager = this;
    }

    void Start() {

        initialize(firstChunk);
    }


    public void initialize(string firstChunk) {

        // Check if there is already a level chunk to start from            
        foreach (Transform child in transform) {

            if (child.GetComponent<Chunk>() && child.GetComponent<Chunk>().startChunk) {

                chunkList.Add(child.gameObject);
            }

            // Go through all children chunks just to turn them off and find the start chunk
            if (child.GetComponent<Chunk>() && !child.GetComponent<Chunk>().startChunk) {

                // Add it to our list of chunks
                originalChunkList.Add(child.GetComponent<Chunk>());

                // All chunks need to be run their awake and start methods so they need to be activated temporarily. LevelChunk will disable it's own gameobject once initialization is complete
                if (!child.GetComponent<Chunk>().initialized)
                    child.gameObject.SetActive(true);
            }
            else if (child.GetComponent<Chunk>() && child.name.Equals("LevelChunk (Start)")) {
                startChunk = child.GetComponent<Chunk>();
            }
        }

        Chunk chunk = findChunk(firstChunk);
        if (chunk == null)
            Debug.Log("Level Manager: initialize() : Can't find firstChunk: " + firstChunk);
        else
            activeChunk = chunk;                 

        createNextChunk();
    }

    // Instantiate a chunk in front of the current one
    void createNextChunk() {
                

        Vector3 newChunkPosition = Vector3.zero;      // The first chunk created should be at zeros
        
        // If this is not the first chunk
        if (newestChunk != null) newChunkPosition = new Vector3(newestChunk.endPoint.transform.position.x, newestChunk.endPoint.transform.position.y, newestChunk.endPoint.transform.position.z);


        Chunk newChunk = Instantiate(activeChunk, newChunkPosition, Quaternion.identity) as Chunk;
        newChunk.motherChunk = activeChunk;
        newChunk.name += " " + Mathf.Floor(Random.Range(0,101));    // Random number used to differentiate between chunks at runtime
        
        // Move        
        newChunk.gameObject.SetActive(true);
        newChunk.updatePositions();

        chunkList.Add(newChunk.gameObject);

        if (chunkList.Count >= totalChunks) {
            GameObject chunkToDestroy = chunkList[0];
            chunkList.Remove(chunkToDestroy);
            Destroy(chunkToDestroy, 1f);
        }

        // Variables
        lastChunk = newestChunk;
        newestChunk = newChunk;
    }


    // When the next chunk in the list has been changed via a switch, replace the chunk
    void updateNextChunk() {

        var newChunkPosition = newestChunk.transform.position;

        Chunk newChunk = Instantiate(activeChunk, newChunkPosition, Quaternion.identity) as Chunk;
        newChunk.motherChunk = activeChunk;
        newChunk.updatePositions();

        newChunk.gameObject.SetActive(true);
        Destroy(newestChunk.gameObject);

        // Variables        
        newestChunk = newChunk;   
    }    

    // Player collides with a level chunk collider
    public void playerTrigger(string transitionToChunk, ChunkCollider chunkCollider) {
                

        if (!transitionToChunk.Equals("")) {

            Chunk chunk = findChunk(transitionToChunk);

            if (chunk == null)
                Debug.Log("Level Manager: playerTrigger with transition : The provided chunk was not found in the original chunk list");
            else {

                activeChunk = chunk;

                if (chunkCollider.parentChunk == currentChunk)      // If this is true, it means we already went through a chunk collider for this chunk and there is already a chunk in front of this one
                    updateNextChunk();
                else
                    createNextChunk();
            }
        }                
        else
            createNextChunk();                
    }

    public void orbGet(string chunkName) {

        Chunk chunk = findChunk(chunkName);

        if (chunk == null) {
         
            Debug.Log("Level Manager: orbGet() : The orbs chunk was not found in the original chunk list");
        }
        else {

            activeChunk = chunk;
            updateNextChunk();
        }
    }


    public void destroyStartChunk() {

        Destroy(startChunk.gameObject, 30f);
    }

    IEnumerator disableChunk(Chunk chunk) {
        
        yield return new WaitForSeconds(1.0f);

        chunk.gameObject.SetActive(false);
    }

    Chunk findChunk(string chunkName) {

        Chunk foundChunk = null;
        foreach (Chunk levelChunk in originalChunkList) {

            if (levelChunk.name.Equals(chunkName)) {
                foundChunk = levelChunk;
            }
        }

        return foundChunk;
    }
}