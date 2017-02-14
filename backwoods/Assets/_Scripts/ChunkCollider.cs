using UnityEngine;
using System.Collections;

public class ChunkCollider : MonoBehaviour {


    public string transitionTo;

    public Chunk parentChunk { get; set; }


    void Awake() {

        parentChunk = GetComponentInParent<Chunk>();
    }

    void OnTriggerEnter(Collider other) {
        
        if (other.tag.Equals("Ghost")) {

            Manager.chunkManager.playerTrigger(transitionTo, this);
            Destroy(gameObject);
        }
    }
}
