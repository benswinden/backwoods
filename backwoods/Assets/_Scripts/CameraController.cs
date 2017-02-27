using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float moveSpeed;

    public Camera actualCamera;

    [Space]
    public bool animating;
    [Header("Vignette")]
    [Range(0,1)]
    public float v_intensity;
    [Range(0, 1)]
    public float v_smoothness;


    [Space]

    public float bloom_intensity;

    float currentSpeed;



    void Awake() {

        Manager.cameraController = this;
        currentSpeed = moveSpeed;

        init();
    }

    public void init() {

        transform.position = new Vector3(transform.position.x, 0, 0);        
    }

    bool stopping;
    public void stop() {

        stopping = true;
    }

    void Update() {

        Manager.postController.bloom.bloom.softKnee = bloom_intensity;

        if (stopping && currentSpeed > 0)
            currentSpeed -= 0.3f;

        // Camera Movement
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (currentSpeed * Time.deltaTime));

        // Animation
        if (animating) {

            Manager.postController.vignette.intensity = v_intensity;
            Manager.postController.vignette.smoothness = v_smoothness;
        }
        

        if (Input.GetKeyUp(KeyCode.Space))
            GetComponent<Animator>().SetTrigger("Start");

        if (Input.GetKeyUp(KeyCode.Escape)) {

            if (!ended) {
                ended = true;
                GetComponent<Animator>().SetTrigger("FadeToBlackEnd");
            }
            else {

                ended = false;
                GetComponent<Animator>().SetTrigger("Escape");
            }
        }

    }

    bool ended;

    public void fadeToBlack() {

        GetComponent<Animator>().SetTrigger("FadeToBlack");

        StartCoroutine("fadeBackFromBlack");
    }

    IEnumerator fadeBackFromBlack() {

        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetTrigger("FadeBackFromBlack");
        
    }
}