using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;         // The point where the player will respawn
    public TextMeshProUGUI messageText;    // Text to display when the player collides
    public float textDisplayDuration = 2f; // Duration to display the text message

    private void Start()
    {
        // Ensure the message text is hidden when the game starts
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with this object
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit the object! Respawning...");

            // Respawn the player and show the text message
            RespawnPlayer(other.gameObject);
            StartCoroutine(ShowMessage());
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Get relevant components
        Rigidbody rb = player.GetComponent<Rigidbody>();
        CharacterController charController = player.GetComponent<CharacterController>();

        // Disable player movement temporarily
        if (charController != null) charController.enabled = false;
        if (rb != null)
        {
            rb.isKinematic = true; // Temporarily disable Rigidbody physics
            rb.velocity = Vector3.zero; // Reset velocity to avoid unintended movement
            rb.angularVelocity = Vector3.zero;
        }

        // Set player position and rotation to the respawn point
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        // Re-enable components after repositioning
        if (rb != null) rb.isKinematic = false;
        if (charController != null) charController.enabled = true;
    }

    IEnumerator ShowMessage()
    {
        // Show the message text
        messageText.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(textDisplayDuration);

        // Hide the message text
        messageText.gameObject.SetActive(false);
    }
}
