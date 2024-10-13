using UnityEngine;

public class SyringeCollisionHandler : MonoBehaviour
{
    public string syringeTag = "Syringe"; // The tag assigned to the syringe object

    // Reference to guard's components (optional, but more efficient if pre-referenced)
    private guard_animator guardAnimator;
    private GuardPatrol guardPatrol;

    void Start()
    {
        // Cache references to the components (you can also drag and drop in the Inspector if you prefer)
        guardAnimator = GetComponent<guard_animator>();
        guardPatrol = GetComponent<GuardPatrol>();
    }

    // Detect collision with the syringe object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(syringeTag))
        {
            // Call the guard's Die method to play the dying animation
            if (guardAnimator != null)
            {
                guardAnimator.Die();  // Trigger the dying animation
            }

            // Disable the patrol script to stop the guard's movement
            if (guardPatrol != null)
            {
                guardPatrol.enabled = false;  // Disable the patrol script
            }

            // Optionally destroy or disable the syringe object upon impact
            Destroy(collision.gameObject);
        }
    }
}
