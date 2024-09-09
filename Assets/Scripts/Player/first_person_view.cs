using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_person_view : MonoBehaviour
{
    public Transform player; // player to which the camera is attached
    public float mouse_sensitivity = 4f;
    float camera_vert_rot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float input_x = Input.GetAxis("Mouse X") * mouse_sensitivity;
        float input_y = Input.GetAxis("Mouse Y") * mouse_sensitivity;

        camera_vert_rot -= input_y;
        camera_vert_rot = Mathf.Clamp(camera_vert_rot, -90f, 90f);
        transform.localEulerAngles = Vector3.right * camera_vert_rot;

        player.Rotate(Vector3.up * input_x);
    }
}
