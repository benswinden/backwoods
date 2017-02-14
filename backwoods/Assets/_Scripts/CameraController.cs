using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Vector3 velocity = Vector3.zero;

    public float smoothTime = 0.15f;

    [Space]

    public bool verticalMaxEnabled = false;
    public float verticalMax = 0f;
    public bool verticalMinEnabled = false;
    public float verticalMin = 0f;

    [Space]

    public bool horizontalMaxEnabled = false;
    public float horizontalMax = 0f;
    public bool horizontalMinEnabled = false;
    public float horizontalMin = 0f;

    void FixedUpdate() {

        if (target) {

            transform.position = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z);

            //Vector3 targetPosition = target.position;

            //if (verticalMinEnabled && verticalMaxEnabled) {
            //    targetPosition.y = Mathf.Clamp(target.position.y, verticalMin, verticalMax);
            //}
            //else if (verticalMinEnabled) {
            //    targetPosition.y = Mathf.Clamp(target.position.y, verticalMin, target.position.y);
            //}
            //else if (verticalMaxEnabled) {
            //    targetPosition.y = Mathf.Clamp(target.position.y, target.position.y, verticalMax);
            //}

            //if (horizontalMinEnabled && horizontalMaxEnabled) {
            //    targetPosition.x = Mathf.Clamp(target.position.z, horizontalMin, horizontalMax);
            //}
            //else if (horizontalMinEnabled) {
            //    targetPosition.x = Mathf.Clamp(target.position.z, horizontalMin, target.position.z);
            //}
            //else if (horizontalMaxEnabled) {
            //    targetPosition.x = Mathf.Clamp(target.position.z, target.position.z, horizontalMax);
            //}

            //targetPosition.x = transform.position.x;

            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime * Time.deltaTime);
        }
    }
}