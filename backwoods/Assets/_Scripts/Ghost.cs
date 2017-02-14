using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public float forceAmount;


    Rigidbody rigidbodyComponent;

    void Awake() {

        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    void Update() {

        if (Input.GetKey(KeyCode.RightArrow))
            rigidbodyComponent.AddForce(transform.forward * forceAmount * Time.deltaTime);
    }
}
