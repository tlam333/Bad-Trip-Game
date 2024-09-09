using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiorSauvage : Item {

    [SerializeField] float tranquilizeGuardDuration = 5f;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Slider progressBar;
    [SerializeField] IntoxicationManager intoxicationManager;

    private bool isUsingDior = false;

    public override void Use()
    {
        if (!isUsingDior) {
            isUsingDior = true;

            intoxicationManager.AddIntoxication();
            
            // Neutrilize Guard Logic

            // Initialize and start progress bar
            progressBar.gameObject.SetActive(true); // Show progress bar
            progressBar.maxValue = tranquilizeGuardDuration; // Set max value
            progressBar.value = tranquilizeGuardDuration; // Initialize value

            StartCoroutine(ResetGuardAfterDuration(tranquilizeGuardDuration));
        }
    }

    private IEnumerator ResetGuardAfterDuration(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            progressBar.value = duration - elapsedTime; // Update progress bar value
            yield return null;
        }

        // Reset guard logic

        isUsingDior = false;

        // Hide progress bar
        progressBar.gameObject.SetActive(false);
    }
}