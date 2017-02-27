using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCounters : MonoBehaviour {


    public BeatCounter downBeat;
    public BeatCounter upBeat;
    public BeatCounter offBeat;

    void Awake() {

        Manager.beatCounters = this;
    }


}
