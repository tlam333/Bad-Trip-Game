using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; 
using TMPro; 

public class BeamDetection : MonoBehaviour
{
    public Transform respawnPoint; // Reference to the respawn point
    public TextMeshProUGUI beamDetectedText; // Reference to the TextMeshProUGUI or UI Text component
    public float textDisplayDuration = 2f; // Duration to display the text

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected in the beam!");
            StartCoroutine(ShowText(other.gameObject));
            RespawnPlayer(other.gameObject);
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
        // Move the player to the respawn point's position
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation; // Optional: reset the player's rotation as well
    }
}
