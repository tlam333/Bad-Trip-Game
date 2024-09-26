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
            guard_animator guard = collision.gameObject.GetComponent<guard_animator>();
            if (guard != null)
            {
                guard.Die();
            }

            // Optionally, destroy or disable the item
            Destroy(this.gameObject);
        }
    }
}
