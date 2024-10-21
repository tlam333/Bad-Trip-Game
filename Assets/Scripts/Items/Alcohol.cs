using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Alcohol : ItemTest {

    [SerializeField] float invisibilityDuration = 5f;
    // [SerializeField] PlayerMovement playerMovement;

    private bool isUsingAlcohol = false;

    public ParticleSystem alcoholParticleSystem; // Reference to the particle system

    public override void Use(IntoxicationManager intoxicationManagerRef)
    {
        if (!isUsingAlcohol) {
            Debug.Log("Using Alcohol");
            isUsingAlcohol = true;

            intoxicationManagerRef.AddIntoxication();

            // Play the particle system
            alcoholParticleSystem.Play();

            // Make invisible
            /* HERE */

            // Initialize and start progress bar
            itemDurationBar.gameObject.SetActive(true); // Show progress bar
            itemDurationBar.maxValue = invisibilityDuration; // Set max value
            itemDurationBar.value = invisibilityDuration; // Initialize value

            // Stop the particle system after 3 seconds
            StartCoroutine(StopParticleSystemAfterDuration(4f));

            StartCoroutine(ResetVisibilityAfterDuration(invisibilityDuration));
        }
    }

    private IEnumerator StopParticleSystemAfterDuration(float delay)
    {
        yield return new WaitForSeconds(delay);
        alcoholParticleSystem.Stop();
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