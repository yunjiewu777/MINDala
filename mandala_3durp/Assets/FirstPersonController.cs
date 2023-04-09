using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;

    private float xRotation = 0.0f;
    private Transform cameraTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(xInput, 0, zInput);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
