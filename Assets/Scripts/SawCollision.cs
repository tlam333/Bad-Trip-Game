using System.Collections;
using UnityEngine;
using TMPro;

public class SawCollision : MonoBehaviour
{
    public Transform respawnPoint; // The point where the player will respawn
    public TextMeshProUGUI hitText;
    public float textDisplayDuration = 2f;

    private void Start()
    {
        // Ensure the text is hidden at the start
        hitText.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the saw is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by the saw! Respawning...");

            // Respawn the player
            RespawnPlayer(other.gameObject);
            StartCoroutine(ShowText(other.gameObject));
        }
    }

    IEnumerator ShowText(GameObject player)
    {
        // Show the text
        hitText.gameObject.SetActive(true);

        // Wait for the duration
        yield return new WaitForSeconds(textDisplayDuration);

        // Hide the text
        hitText.gameObject.SetActive(false);
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
}
