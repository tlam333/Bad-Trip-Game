using System.Collections;
using UnityEngine;
using TMPro;

public class BeamDetection : MonoBehaviour
{
    public Transform respawnPoint; // Reference to the respawn point
    public TextMeshProUGUI beamDetectedText; // Reference to the TextMeshProUGUI or UI Text component
    public float textDisplayDuration = 2f; // Duration to display the text
    private bool isActive = true;
   void OnTriggerEnter(Collider other)
{
    // Check if the object that entered the trigger is the player and if the script is active
    if (other.CompareTag("Player") && isActive)
    {
        Debug.Log("Player detected in the beam!");
        RespawnPlayer(other.gameObject);
        StartCoroutine(ShowText(other.gameObject));
    }
}


    IEnumerator ShowText(GameObject player)
    {
        // Show the text
        beamDetectedText.gameObject.SetActive(true);

        // Wait for the duration
        yield return new WaitForSeconds(textDisplayDuration);

        // Hide the text
        beamDetectedText.gameObject.SetActive(false);
    }

    void RespawnPlayer(GameObject player)
{
    
    // Get relevant components
    Rigidbody rb = player.GetComponent<Rigidbody>();
    PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
    CharacterController charController = player.GetComponent<CharacterController>();

    // Disable the movement script to prevent immediate input overrides
    if (movementScript != null)
    {
        movementScript.enabled = false;
    }

    // Disable the Character Controller to ensure manual positioning is not overridden
    if (charController != null)
    {
        charController.enabled = false;
    }

    // Disable Rigidbody physics temporarily to avoid conflicts
    if (rb != null)
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    // Set the player's position and rotation to the respawn point
    player.transform.position = respawnPoint.position;
    player.transform.rotation = respawnPoint.rotation;

    // Re-enable Rigidbody physics interactions after respawn
    if (rb != null)
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.velocity = Vector3.zero; // Reset velocity to ensure no unintended movement after respawn
        rb.angularVelocity = Vector3.zero; // Reset angular velocity
    }

    // Re-enable Character Controller after the player has been repositioned
    if (charController != null)
    {
        charController.enabled = true;
    }

    // Re-enable the movement script to restore normal player movement
    if (movementScript != null)
    {
        movementScript.enabled = true;
    }
    
}
public void DisableBeamDetection()
    {
        isActive = false;
        Debug.Log("Beam detection disabled.");
    }
}
