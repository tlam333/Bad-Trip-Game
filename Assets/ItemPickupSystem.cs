using System;
using UnityEngine;

public class ItemPickupSystem : MonoBehaviour
{
    public float sphereRadius = 0.5f; // The radius of the sphere
    public float maxDistance = 5f;    // The max distance for detection
    public KeyCode pickupKey = KeyCode.F; // Key to pick up items
    public KeyCode dropKey = KeyCode.G;   // Key to drop items

    public IntoxicationManager intoxicationManager;

    public Camera playerCamera;      // The player's camera
    public Transform itemPos;        // The position where the item will be held

    private ItemTest currentlyHeldItem;  // Reference to the currently held item

    private void Update()
    {
        DetectPickupableObject();
        
        // Try to pick up an item if the pickup key is pressed
        if (Input.GetKeyDown(pickupKey))
        {
            TryPickupItem();
        }

        // Drop the currently held item if the drop key is pressed
        if (Input.GetKeyDown(dropKey))
        {
            TryDropItem();
        }

        if (Input.GetMouseButtonDown(0) && currentlyHeldItem) {
            currentlyHeldItem.Use(intoxicationManager);
        }
    }

    // Detect if there is any item in front of the player that can be picked up
    private void DetectPickupableObject()
    {
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        // Perform a SphereCast to detect items within range
        if (Physics.SphereCast(origin, sphereRadius, direction, out RaycastHit hitInfo, maxDistance))
        {
            ItemTest pickupable = hitInfo.collider.GetComponent<ItemTest>();
            if (pickupable != null && currentlyHeldItem == null)
            {
                // Show a UI prompt like "Press F to pick up"
                Debug.Log("Looking at a pickupable item: " + hitInfo.collider.name);
            }
        }
    }

    // Try to pick up the item if it is detected
    private void TryPickupItem()
    {
        if (currentlyHeldItem != null) return; // Only allow picking up one item at a time

        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        // Perform the SphereCast again to check for pickupable items
        if (Physics.SphereCast(origin, sphereRadius, direction, out RaycastHit hitInfo, maxDistance))
        {
            ItemTest pickupable = hitInfo.collider.GetComponent<ItemTest>();
            if (pickupable != null)
            {
                // Pick up the item
                Debug.Log("Picked up: " + hitInfo.collider.name);
                currentlyHeldItem = pickupable;

                pickupable.Pickup(itemPos);
            }
        }
    }

    // Drop the currently held item
    private void TryDropItem()
    {
        if (currentlyHeldItem != null)
        {
            Debug.Log("Dropped: " + currentlyHeldItem.name);
            currentlyHeldItem.Drop();
            currentlyHeldItem = null;  // Clear the reference after dropping the item
        }
    }
}
