using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Alcohol : Item {

    [SerializeField] float invisibilityDuration = 5f;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Slider progressBar;
    [SerializeField] IntoxicationManager intoxicationManager;

    private bool isUsingAlcohol = false;

    public override void Use()
    {
        if (!isUsingAlcohol) {
            isUsingAlcohol = true;

            intoxicationManager.AddIntoxication();
            
            // make invisible
            /* HERE */

            // Initialize and start progress bar
            progressBar.gameObject.SetActive(true); // Show progress bar
            progressBar.maxValue = invisibilityDuration; // Set max value
            progressBar.value = invisibilityDuration; // Initialize value

            StartCoroutine(ResetVisibilityAfterDuration(invisibilityDuration));
        }
    }

    private IEnumerator ResetVisibilityAfterDuration(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            progressBar.value = duration - elapsedTime; // Update progress bar value
            yield return null;
        }

        // make visible
        /* HERE */

        isUsingAlcohol = false;

        // Hide progress bar
        progressBar.gameObject.SetActive(false);
    }
}