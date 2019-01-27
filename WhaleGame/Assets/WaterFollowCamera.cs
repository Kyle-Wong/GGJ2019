using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollowCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start() {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate() {
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, transform.position.z);
    }
}
