using UnityEngine;

public class SimpleGuardPatrol : MonoBehaviour
{
    public Transform pointA; // Assign in the Inspector
    public Transform pointB; // Assign in the Inspector
    public float speed = 2f;
    public float rotationSpeed = 5f; // Speed of rotation

    private Vector3 target;
    private Quaternion targetRotation;

    void Start()
    {
        // Start by setting the GameObject at pointA
        transform.position = pointA.position;
        target = pointB.position;
        SetTargetRotation(target);
    }

    void Update()
    {
        // Move the GameObject towards the target point
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Rotate smoothly towards the target direction
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the GameObject has reached the target point
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
             // Snap position to the exact target to avoid drift
            transform.position = target;
            // Swap the target point when reached
            target = (target == pointA.position) ? pointB.position : pointA.position;

            // Set the new target rotation for the new direction
            SetTargetRotation(target);
        }
    }

    void SetTargetRotation(Vector3 targetPosition)
{
    Vector3 direction = (targetPosition - transform.position).normalized;
    Quaternion newTargetRotation = Quaternion.LookRotation(direction);

    // Only update rotation if the angle difference is significant
    if (Quaternion.Angle(transform.rotation, newTargetRotation) > 0.1f)
    {
        targetRotation = newTargetRotation;
    }
}

}
