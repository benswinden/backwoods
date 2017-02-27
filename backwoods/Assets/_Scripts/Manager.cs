using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing.Utilities;

public class Manager : MonoBehaviour {

    public static ChunkManager chunkManager;
    public static PostProcessingController postController;

    public static CameraController cameraController;
    public static Metronome metronome;
    public static BeatCounters beatCounters;

    void Update() {

        //if (Input.GetKeyUp(KeyCode.S)) {

        //    string filename = ScreenShotName(0);
        //    Application.CaptureScreenshot(filename, 2);
        //    Debug.Log(string.Format("Screen Captured: {0}", filename));
        //}
    }

    public static string ScreenShotName(int number) {

        return string.Format("{0}/Screenshots/{1} + " + number + ".png",
            Application.dataPath,
            System.DateTime.Now.ToString("yyyy-MM-dd__HH-mm-ss"));
    }
}
