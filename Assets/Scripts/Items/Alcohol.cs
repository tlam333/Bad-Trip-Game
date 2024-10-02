using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Alcohol : ItemTest {

    [SerializeField] float invisibilityDuration = 5f;
    // [SerializeField] PlayerMovement playerMovement;

    private bool isUsingAlcohol = false;

    public override void Use(IntoxicationManager intoxicationManagerRef)
    {
        if (!isUsingAlcohol) {
            Debug.Log("Using Alcohol");
            isUsingAlcohol = true;

            intoxicationManagerRef.AddIntoxication();
            
            // make invisible
            /* HERE */

            // Initialize and start progress bar
            itemDurationBar.gameObject.SetActive(true); // Show progress bar
            itemDurationBar.maxValue = invisibilityDuration; // Set max value
            itemDurationBar.value = invisibilityDuration; // Initialize value

            StartCoroutine(ResetVisibilityAfterDuration(invisibilityDuration));
        }
    }

    private IEnumerator ResetVisibilityAfterDuration(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            itemDurationBar.value = duration - elapsedTime; // Update progress bar value
            yield return null;
        }

        // make visible
        /* HERE */

        isUsingAlcohol = false;

        // Hide progress bar
        itemDurationBar.gameObject.SetActive(false);
    }
}