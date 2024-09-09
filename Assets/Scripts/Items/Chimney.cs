using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Chimney : Item {

    [SerializeField] float jumpBoostDuration = 5f;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Slider progressBar;
    [SerializeField] IntoxicationManager intoxicationManager;

    private bool isUsingBong = false;


    public override void Use()
    {
        if (!isUsingBong) {
            isUsingBong = true;

            intoxicationManager.AddIntoxication();

            // Set Jump Height
            playerMovement.SetJumpHeight(playerMovement.GetJumpHeight() * 3);

            // Set Gravity
            playerMovement.SetGravity(playerMovement.GetGravity() / 8);

            // Initialize and start progress bar
            progressBar.gameObject.SetActive(true); // Show progress bar
            progressBar.maxValue = jumpBoostDuration; // Set max value
            progressBar.value = jumpBoostDuration; // Initialize value

            StartCoroutine(ResetJumpHeightAfterDuration(jumpBoostDuration));
        }
    }

    private IEnumerator ResetJumpHeightAfterDuration(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            progressBar.value = duration - elapsedTime; // Update progress bar value
            yield return null;
        }

        // Reset Jump Height
        playerMovement.SetJumpHeight(playerMovement.GetJumpHeight() / 3);

        // Reset Gravity
        playerMovement.SetGravity(playerMovement.GetGravity() * 8);

        isUsingBong = false;

        // Hide progress bar
        progressBar.gameObject.SetActive(false);
    }
}