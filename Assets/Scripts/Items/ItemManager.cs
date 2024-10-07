using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    [SerializeField] KeyCode PICK_UP_KEY = KeyCode.F;
    [SerializeField] KeyCode DROP_KEY = KeyCode.G;
    [SerializeField] KeyCode OPEN_KEY = KeyCode.R;
    [SerializeField] int USE_MOUSE_BUTTON = 0;

    private bool canUseItem;

    //private PlayerUI playerUI;
    private String syringeTag = "Syringe";
    private String itemTag = "Item";
    private String DoorTag = "Door";

    private List<Collider> itemsInRange;

    // Current item in hand
    private Collider currentHand;

    private bool canPickUpItem;

    [SerializeField] float throwMultiplier = 150f; 

    [SerializeField] float throwUpMultiplier = 1.5f;

    [SerializeField] Transform playerView;

    [SerializeField] private GameObject door;
    private bool doorOpen;
    private bool onDoorKnob;

    void Start() {
        itemsInRange = new List<Collider>();
        canPickUpItem = true;
        currentHand = null;
        onDoorKnob = false;
        canUseItem = true;
        // playerUI = GetComponent<PlayerUI>();
    }

    void Update() {

        if (itemsInRange.Count > 0) canPickUpItem = true; else canPickUpItem = false;

        if (Input.GetKey(PICK_UP_KEY) && canPickUpItem && !currentHand) {
            HandlePickupItem();
        }

        // If drop keybind is down and you're holding an item then drop
        if (Input.GetKey(DROP_KEY) && currentHand) {
            HandleDropItem();
        }

        if (Input.GetMouseButtonDown(USE_MOUSE_BUTTON) && canUseItem && currentHand)
        {
            HandleUseItem();
            canUseItem = false;  // Disable until the button is released
        }

        // Reset the flag when the mouse button is released
        if (Input.GetMouseButtonUp(USE_MOUSE_BUTTON))
        {
            canUseItem = true;
        }

        if (Input.GetKey(OPEN_KEY) && onDoorKnob) {
            OpenDoor();
        }
    }


    private void OnTriggerEnter(Collider other) {

        // Debug.Log("hihi");
        //playerUI.UpdateText(string.Empty);

        if (other.gameObject.CompareTag(DoorTag)) {
            //playerUI.UpdateText("Press R bro");
            onDoorKnob = true;
        }
        // Check if the other item is already in the list and if not add it to the list
        if ((other.gameObject.CompareTag(itemTag) || other.gameObject.CompareTag(syringeTag)) && !itemsInRange.Contains(other)) {
            //playerUI.UpdateText("Press F to pick up");
            itemsInRange.Add(other);
        }
        
    }

    private void OnTriggerExit(Collider other) {
        if ((other.gameObject.CompareTag(itemTag) || other.gameObject.CompareTag(syringeTag)) && itemsInRange.Contains(other)) {

            itemsInRange.Remove(other);
        }
    }

    private void OpenDoor() {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }



    private void HandlePickupItem() {
        Collider item = itemsInRange[0];
            
        // Remove the item from the list
        itemsInRange.RemoveAt(0);

        // Perform any additional logic such as destroying the item
        item.transform.SetParent(playerView);

        currentHand = item;

        Camera playerCamera = playerView.GetComponent<Camera>();

        item.transform.localPosition = new UnityEngine.Vector3(0.75f, -0.45f, 0.7f);

        item.transform.rotation = playerCamera.transform.rotation;
        item.transform.localRotation *= UnityEngine.Quaternion.Euler(-90, 0, 0);

        HandlePickupPhysics(item);
    }

    private void HandlePickupPhysics(Collider item) {
        Rigidbody rb = item.GetComponent<Rigidbody>();

        // If found disable physics
        if (rb) {
            
            rb.isKinematic = true;  // Disable physics simulation on the object
            rb.useGravity = false;  // Disable gravity
            // rb.velocity = UnityEngine.Vector3.zero;  // Stop any movement
            // rb.angularVelocity = UnityEngine.Vector3.zero;  // Stop any rotation
        }
    }

    private void HandleDropItem() {
        currentHand.transform.SetParent(null);

        HandleDropPhysics(currentHand);

        currentHand = null;
    }

    private void HandleDropPhysics(Collider item) {
        Rigidbody rb = item.GetComponent<Rigidbody>();

        // If found enable physics
        if (rb) {
            
            rb.isKinematic = false;  // Disable physics simulation on the object
            rb.useGravity = true;  // Disable gravity
            
            // Get vector of facing direction
            Camera playerCamera = playerView.GetComponent<Camera>();
            UnityEngine.Vector3 cameraDirection = playerCamera.transform.forward;

            UnityEngine.Vector3 upVector = new UnityEngine.Vector3(0, 1f * throwUpMultiplier, 0);

            // Add throwing force
            rb.AddForce((cameraDirection + upVector) * throwMultiplier);
        }


    }

    private void HandleUseItem() {
        Item currItem = currentHand.gameObject.GetComponent<Item>();

        if (currItem) {
            currItem.Use();
        } else {
            Debug.Log("Error using item: " + currentHand.gameObject.name);
        }
    }

    public bool IsHoldingItem() {
        return currentHand != null;
    }
}