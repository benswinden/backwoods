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

    float fastSpeed = 1f;
    float currentSpeed;



    void Awake() {

        currentSpeed = moveSpeed;

        transform.position = new Vector3(transform.position.x, 0, 0);        
    }
   
    void Update() {

        // Camera Movement
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + currentSpeed);

        // Animation
        if (animating) {

            Manager.postController.vignette.intensity = v_intensity;
            Manager.postController.vignette.smoothness = v_smoothness;
        }


        if (Input.GetKeyUp(KeyCode.Tab)) {

            if (currentSpeed == moveSpeed)
                currentSpeed = fastSpeed;
            else
                currentSpeed = moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Q)) {

            GetComponent<Animator>().SetTrigger("FadeToBlack");
        }
    }
}