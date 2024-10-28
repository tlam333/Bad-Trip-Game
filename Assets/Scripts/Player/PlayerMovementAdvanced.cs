using UnityEngine;

public class PlayerMovementAdvanced : MonoBehaviour
{
    // Public Variables
    public float moveSpeed = 3f;
    public float sprintSpeed = 5f; // Speed when sprinting
    public float crouchSpeed = 2f; // Speed when crouching
    public float jumpHeight = 1f;
    public float gravity = -9.81f;

    public float crouchHeight = 1f; // Height when crouching
    public float standHeight = 2f;  // Height when standing
    public float crouchTransitionSpeed = 5f; // Speed of transition between crouch and stand

    // Private Variables
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching;
    private float targetHeight; // Height to lerp towards

    // Ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform headCheck;  // Transform for checking obstacles above the player's head
    public float headCheckRadius = 0.3f; // Radius for head check
    public Transform orientation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetHeight = standHeight;

        // Set the move speed based on the value from the main menu
        moveSpeed = MainMenu.playerSpeed;
        sprintSpeed = moveSpeed * 1.5f;   // Adjust sprint speed accordingly
        crouchSpeed = moveSpeed * 0.75f;  // Adjust crouch speed accordingly
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keep the player grounded
        }

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Determine if the player is sprinting, crouching, or moving normally
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.C))
        {
            // Crouching
            currentSpeed = crouchSpeed;
            targetHeight = crouchHeight; // Set target height to crouch height
            isCrouching = true;
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            // Check if there is enough space to stand up
            if (!Physics.CheckSphere(headCheck.position, headCheckRadius, groundMask))
            {
                targetHeight = standHeight; // Set target height to stand height
                isCrouching = false;
            }
        }

        // Smoothly transition player height
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);

        // Sprinting
        if (!isCrouching && Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // Apply sprint speed if Left Shift is pressed
        }

        // Apply movement
        Vector3 moveDirection = orientation.forward * z + orientation.right * x;
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public float GetJumpHeight() {
        return jumpHeight;
    }

    public void SetJumpHeight(float jumpHeight) {
        this.jumpHeight = jumpHeight;
    }
}
