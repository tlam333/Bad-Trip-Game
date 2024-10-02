using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ItemTest : MonoBehaviour
{
    private bool pickedUp;

    private Transform itemHolder;  // The position where the item will be held
    public string playerTag = "Player";  // Tag to identify the player

    // Intoxication
    public Slider itemDurationBar;

    private Rigidbody rb;
    private Collider itemCollider;
    private Collider playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        itemCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (pickedUp)
        {
            // Lock the item to the holder's position and rotation
            transform.position = itemHolder.position;
            transform.rotation = itemHolder.rotation;
        }
    }


    public virtual void Use(IntoxicationManager intoxicationManagerRef) {
        Debug.Log("Using item");
    }

    

    public void Pickup(Transform itemHolderRef)
    {
        pickedUp = true;
        itemHolder = itemHolderRef;

        // Find the player's collider using the tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerCollider = player.GetComponent<CharacterController>();
            if (playerCollider != null)
            {
                // Ignore collision between the item and the player
                Physics.IgnoreCollision(itemCollider, playerCollider, true);
            }
        }

        // Parent the item to the item holder
        transform.SetParent(itemHolder);

        // Disable physics
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Drop()
    {
        pickedUp = false;

        // Re-enable collision with the player
        if (playerCollider != null)
        {
            Physics.IgnoreCollision(itemCollider, playerCollider, false);
        }

        // Hide the itemDurationBar on Drop
        if (itemDurationBar) {
            itemDurationBar.gameObject.SetActive(false);
        }

        // Unparent the item from the holder
        transform.SetParent(null);

        // Re-enable physics
        rb.isKinematic = false;
        rb.useGravity = true;

        // Optionally apply force to "throw" the item
        rb.AddForce(itemHolder.forward * 10f, ForceMode.Impulse);  // Example throw force
    }
}
