using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Metronome : MonoBehaviour {

    public double bpm = 140.0F;

    [Space]

    public List<GameObject> objects;            // List of objects to send tick events to


    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F;
    bool ticked = false;

    // Setting BPM via input
    bool startedTiming;
    float time;


    void Awake() {

        Manager.metronome = this;
    }

    void Start() {

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;

        nextTick = startTick + (60.0 / bpm);
    }

    void Update() {

        if (!startedTiming) {

            if (Input.GetKeyDown(KeyCode.Z)) {

                StopCoroutine("timingRoutine");
                StartCoroutine("timingRoutine");

                doTick();

                time = 0;
                startedTiming = true;
            }
        }
        else {

            time += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Z)) {

                StopCoroutine("timingRoutine");

                startedTiming = false;

                bpm = 60 / time;

                doTick();
            }
        }
    }

    IEnumerator timingRoutine() {

        yield return new WaitForSeconds(1.5f);

        startedTiming = false;
    }


    void LateUpdate() {

        if (!startedTiming && !ticked && nextTick >= AudioSettings.dspTime) {

            doTick();
        }
    }

    void doTick() {

        ticked = true;

        foreach (GameObject obj in objects) {
            
            obj.SendMessage("OnTick");
        } 
    }

    // Just an example OnTick here
    void OnTick() {

        // GetComponent<AudioSource>().Play();
    }

    void FixedUpdate() {

        double timePerTick = 60.0 / bpm;
        double dspTime = AudioSettings.dspTime;

        while (dspTime >= nextTick) {
            ticked = false;
            nextTick += timePerTick;
        }

    }
}

/// hi u cute