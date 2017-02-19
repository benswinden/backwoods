using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Chunk : MonoBehaviour {

    public bool startChunk;
    public bool dontDeactivate;

    [Space]

    public GameObject startPoint;
    public GameObject endPoint;

    GameObject startObject;
    GameObject endObject;    
    
    public Chunk motherChunk { get; set; }

    public bool initialized { get; set; }

    void Awake() {
        
        
        updatePositions();

        initialized = true;         // Stored so that the LevelManager doesn't enable this on game start if it was left enabled in editor

        if (!dontDeactivate && !startChunk && motherChunk == null)
            gameObject.SetActive(false);
    }

    public void updatePositions() {

        if (startObject != null && endObject != null) {
            startPoint = startObject;
            endPoint = endObject;
        }
    }    
}