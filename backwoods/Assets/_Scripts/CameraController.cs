using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float moveSpeed;

    void Update() {

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed);

    }
}