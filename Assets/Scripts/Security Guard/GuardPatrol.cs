using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Transform pointA;    // The first point to patrol to
    public Transform pointB;    // The second point to patrol to
    public float speed = 2f;    // The speed of the guard
    public float rotationSpeed = 5f; // Speed of rotation

    private Vector3 targetPosition; // The current target position
    private bool movingToPointB = true; // Track if moving to point B

    void Start()
    {
        // Set the initial target to point B
        targetPosition = pointB.position;
    }

    void Update()
    {
        // Move the guard toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate the guard to face the target position
        Vector3 direction = targetPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // Check if the guard has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Switch to the other target
            if (movingToPointB)
            {
                targetPosition = pointA.position;
                movingToPointB = false;
            }
            else
            {
                targetPosition = pointB.position;
                movingToPointB = true;
            }
        }
    }
}
