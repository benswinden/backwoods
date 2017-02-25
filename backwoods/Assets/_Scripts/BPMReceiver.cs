using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMReceiver : MonoBehaviour {

    Animator animator;
    
    void Awake() {

        animator = GetComponent<Animator>();
    }

    void Start() {

        Manager.metronome.objects.Add(gameObject);
    }

    void OnDestroy() {

        Manager.metronome.objects.Remove(gameObject);
    }

    void OnTick() {

        animator.Play("Animation");
    }
}
