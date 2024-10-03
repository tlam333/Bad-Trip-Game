using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : MonoBehaviour
{
    private Camera playerCamera; // Reference to the Camera component
    public float mouseSensitivity = 4f;
    private float cameraVertRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>(); // Get the Camera component on this GameObject
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVertRot -= inputY;
        cameraVertRot = Mathf.Clamp(cameraVertRot, -90f, 90f);
        playerCamera.transform.localEulerAngles = Vector3.right * cameraVertRot;

        transform.Rotate(Vector3.up * inputX); // Rotate the parent object (which holds the camera)
    }
}
