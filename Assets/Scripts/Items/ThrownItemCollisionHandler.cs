using UnityEngine;

public class ThrownItemCollisionHandler : MonoBehaviour
{
    private ItemManager itemManager;

    public void Initialize(ItemManager manager)
    {
        itemManager = manager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Guard"))
        {
            // Call the guard's Die method to play the dying animation
            guard_animator guardAnimator = collision.gameObject.GetComponent<guard_animator>();
            GuardPatrol guardPatrol = collision.gameObject.GetComponent<GuardPatrol>();

            if (guardAnimator != null)
            {
                guardAnimator.Die();  // Trigger the dying animation
            }

            if (guardPatrol != null)
            {
                guardPatrol.enabled = false;  // Disable the patrol script to stop the guard's movement
            }

            // Optionally, destroy or disable the item
            Destroy(this.gameObject);
        }
    }
}
