using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float clampNeg, clampPos;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clampNeg, clampPos);
        transform.localRotation = Quaternion.Euler(-xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


    }
}
