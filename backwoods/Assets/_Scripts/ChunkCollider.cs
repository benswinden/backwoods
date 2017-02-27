using UnityEngine;
using System.Collections;

public class ChunkCollider : MonoBehaviour {

    public bool stopPlayer;

    public string transitionTo;

    public Chunk parentChunk { get; set; }


    void Awake() {

        parentChunk = GetComponentInParent<Chunk>();
    }

    void OnTriggerEnter(Collider other) {

        if (!stopPlayer) {
            if (other.tag.Equals("Ghost")) {

                Manager.chunkManager.playerTrigger(transitionTo, this);
                Destroy(gameObject);
            }
        }
        else {

            Manager.cameraController.stop();
        }
    }
}
