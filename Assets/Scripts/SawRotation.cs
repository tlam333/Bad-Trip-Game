using UnityEngine;

public class SawRotation : MonoBehaviour
{
    public float rotationSpeed = 3050f; // Speed of rotation
    private float maxAngle = 160f;    // Maximum angle of rotation
    private float currentAngle = 0f;  // Track the current angle
    private bool rotatingRight = true; // Track the direction of rotation

    void Update()
    {
        // Calculate the rotation step based on speed and frame time
        float rotationStep = rotationSpeed * Time.deltaTime;

        if (rotatingRight)
        {
            // Rotate toward 180 degrees on the Z-axis
            currentAngle += rotationStep;

            if (currentAngle >= maxAngle)
            {
                // Clamp the angle to 180 and switch direction
                currentAngle = maxAngle;
                rotatingRight = false;
            }
        }
        else
        {
            // Rotate back to 0 degrees on the Z-axis
            currentAngle -= rotationStep;

            if (currentAngle <= 0f)
            {
                // Clamp the angle to 0 and switch direction
                currentAngle = 0f;
                rotatingRight = true;
            }
        }

        // Apply the rotation around the Z-axis
        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}
