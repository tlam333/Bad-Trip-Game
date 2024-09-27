using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guard_animator : MonoBehaviour
{
    public Animator animator;
    public GameObject flashlight; // Reference to the flashlight object attached to the guard

    // Start is called before the first frame update
    void Start()
    {
        // Start walking animation by default
        animator.SetBool("is_walking", true);

        // Set the flashlight's Rigidbody to kinematic initially to prevent it from falling
        if (flashlight != null)
        {
            Rigidbody rb = flashlight.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Prevent the flashlight from falling at the start
            }
        }
    }

    // Call this method to play the dying animation
    public void Die()
    {
        animator.SetBool("is_dying", true);

        // Disable the movement script if it exists
        GuardPatrol guardPatrol = GetComponent<GuardPatrol>();
        if (guardPatrol != null)
        {
            guardPatrol.enabled = false;
        }

        // Handle the flashlight drop and disable its beam detection
        DropFlashlight();
    }

    private void DropFlashlight()
{
    if (flashlight != null)
    {
        // Detach the flashlight from the guard
        flashlight.transform.parent = null;

        // Enable Rigidbody physics to make the flashlight drop naturally
        Rigidbody rb = flashlight.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Enable physics to allow the flashlight to fall
            rb.useGravity = true;
        }

        // Find the cone child that has the beam detection script
            Transform cone = flashlight.transform.Find("cone transparent 2/Cone");
            if (cone != null)
            {
                // Disable the Beam Detection script on the cone
                BeamDetection beamDetection = cone.GetComponent<BeamDetection>();
                if (beamDetection != null)
                {
                    beamDetection.enabled = false; // Disable the beam detection logic
                    beamDetection.DisableBeamDetection();
                }
            }

    }
}

}
