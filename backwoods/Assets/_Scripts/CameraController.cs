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
    
    float currentSpeed;



    void Awake() {

        Manager.cameraController = this;
        currentSpeed = moveSpeed;

        init();
    }

    public void init() {

        transform.position = new Vector3(transform.position.x, 0, 0);        
    }

    void Update() {

        // Camera Movement
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (currentSpeed * Time.deltaTime));

        // Animation
        if (animating) {

            Manager.postController.vignette.intensity = v_intensity;
            Manager.postController.vignette.smoothness = v_smoothness;
        }
        

        if (Input.GetKeyUp(KeyCode.Space))
            GetComponent<Animator>().SetTrigger("Start");

    }

    public void fadeToBlack() {

        GetComponent<Animator>().SetTrigger("FadeToBlack");

        StartCoroutine("fadeBackFromBlack");
    }

    IEnumerator fadeBackFromBlack() {

        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetTrigger("FadeBackFromBlack");
        
    }
}