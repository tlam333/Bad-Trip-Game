using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] KeyCode SPRINT_KEY = KeyCode.LeftShift;
    [SerializeField] KeyCode CROUCH_KEY = KeyCode.C;


    // different speeds
    [SerializeField] float speed = 0.1f;
    [SerializeField] float sprintMultiplier = 2f;
    [SerializeField] float crouchSpeed = 0.1f;

    [SerializeField] float crouchHeight = 0.5f;
    private float normalHeight;
    [SerializeField] float jumpHeight = 2f;

    [SerializeField] float gravity = -9.8f;
    [SerializeField] float crouchTransitionSpeed = 5f;
    [SerializeField] LayerMask cielingLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    private Camera playerView;
    private Vector3 cameraTargetPosition; // Target position for the camera
    private float targetHeight; // Target height for the controller
    private Vector3 targetCenter; // Target center for the controller

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    private void Start() {
        controller = GetComponent<CharacterController>();
        normalHeight = controller.height;
        playerView = GetComponentInChildren<Camera>();
        cameraTargetPosition = playerView.transform.localPosition;
        targetHeight = normalHeight;
        targetCenter = controller.center;
    }

    private void Update() {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Deal with speed calculation
        float currentSpeed = speed;
        if (Input.GetKey(SPRINT_KEY)) // Sprinting
        {
            currentSpeed *= sprintMultiplier;
        }

        if (isCrouching) // Crouch speed
        {
            currentSpeed = crouchSpeed;
        }

        controller.Move(currentSpeed * Time.deltaTime * moveDirection);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Handle crouch as a hold action
        if (Input.GetKey(CROUCH_KEY)) {
            EnterCrouch();
        }
        else if (!CheckObstacleAbove()) {
            ExitCrouch();
        }
        
        // Smoothly interpolate the camera position
        playerView.transform.localPosition = Vector3.Lerp(playerView.transform.localPosition, cameraTargetPosition, crouchTransitionSpeed * Time.deltaTime);

        // Smoothly interpolate the controller's height and center
        controller.height = Mathf.Lerp(controller.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);
        controller.center = Vector3.Lerp(controller.center, targetCenter, crouchTransitionSpeed * Time.deltaTime);
    }

    private void EnterCrouch() {
        if (!isCrouching)
        {
            isCrouching = true;

            // Set the target values for crouch
            targetHeight = crouchHeight;
            targetCenter = new Vector3(controller.center.x, crouchHeight / 2, controller.center.z);
            cameraTargetPosition = new Vector3(playerView.transform.localPosition.x, playerView.transform.localPosition.y - (normalHeight - crouchHeight), playerView.transform.localPosition.z);
        }
    }

    private void ExitCrouch() {
        if (isCrouching)
        {
            isCrouching = false;

            // Set the target values for standing
            targetHeight = normalHeight;
            targetCenter = new Vector3(controller.center.x, normalHeight / 2, controller.center.z);
            cameraTargetPosition = new Vector3(playerView.transform.localPosition.x, playerView.transform.localPosition.y + (normalHeight - crouchHeight), playerView.transform.localPosition.z);
        }
    }

    private bool CheckObstacleAbove() {
        // Define the start and end points for the capsule cast
        Vector3 start = transform.position + Vector3.up * crouchHeight;
        Vector3 end = transform.position + Vector3.up * normalHeight;

        // Perform the capsule cast
        bool isBlocked = Physics.CheckCapsule(start, end, controller.radius, cielingLayer);

        return isBlocked;
    }

    public void SetJumpHeight(float newJumpHeight) {
        jumpHeight = newJumpHeight;
    }

    public float GetJumpHeight() {
        return jumpHeight;
    }

    public void SetGravity(float newGravity) {
        gravity = newGravity;
    }

    public float GetGravity() {
        return gravity;
    }
}