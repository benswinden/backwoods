using UnityEngine;
using System.Collections;
using SynchronizerData;


public class AnimatorBehaviour : MonoBehaviour {

	private Animator anim;
	private BeatObserver beatObserver;

    [Space]

    public bool downBeat;
    public bool upBeat;
    public bool offBeat;

	void Start ()
	{
		anim = GetComponent<Animator>();
		beatObserver = GetComponent<BeatObserver>();

        if (downBeat)
            Manager.beatCounters.downBeat.observers.Add(gameObject);
        if (upBeat)
            Manager.beatCounters.upBeat.observers.Add(gameObject);
        if (offBeat)
            Manager.beatCounters.offBeat.observers.Add(gameObject);
    }

    //void OnEnable() {

    //    if (downBeat && !Manager.beatCounters.downBeat.observers.Contains(gameObject))             
    //        Manager.beatCounters.downBeat.observers.Add(gameObject);        
    //    if (upBeat && !Manager.beatCounters.upBeat.observers.Contains(gameObject))
    //        Manager.beatCounters.upBeat.observers.Add(gameObject);
    //    if (offBeat && !Manager.beatCounters.downBeat.observers.Contains(gameObject))
    //        Manager.beatCounters.offBeat.observers.Add(gameObject);
    //}

    void OnDestroy() {

        if (downBeat)
            Manager.beatCounters.downBeat.observers.Remove(gameObject);
        else if (upBeat)
            Manager.beatCounters.upBeat.observers.Remove(gameObject);
        else if (offBeat)
            Manager.beatCounters.offBeat.observers.Remove(gameObject);
    }

	void Update ()
	{
		if (downBeat && (beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat) {
            anim.Play("Animation");
		}
		if (upBeat && (beatObserver.beatMask & BeatType.UpBeat) == BeatType.UpBeat) {
            anim.Play("Animation");
		}
        if (offBeat && (beatObserver.beatMask & BeatType.OffBeat) == BeatType.OffBeat) {
            anim.Play("Animation");
        }
	}
}
