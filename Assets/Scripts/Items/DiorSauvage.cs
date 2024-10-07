using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DiorSauvage : ItemTest {

    [SerializeField] float tranquilizeGuardDuration = 5f;
    // [SerializeField] PlayerMovement playerMovement;
    public Material opaqueMat;
    public Material transparentMat;


    private bool isUsingDior = false;

    public override void Use(IntoxicationManager intoxicationManagerRef)
    {
        if (!isUsingDior) {
            isUsingDior = true;

            intoxicationManagerRef.AddIntoxication();
            
            // Neutrilize Guard Logic
            ChangeRenderMode(true);

            // Initialize and start progress bar
            itemDurationBar.gameObject.SetActive(true); // Show progress bar
            itemDurationBar.maxValue = tranquilizeGuardDuration; // Set max value
            itemDurationBar.value = tranquilizeGuardDuration; // Initialize value

            StartCoroutine(ResetGuardAfterDuration(tranquilizeGuardDuration));
        }
    }

    private IEnumerator ResetGuardAfterDuration(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            itemDurationBar.value = duration - elapsedTime; // Update progress bar value
            yield return null;
        }

        isUsingDior = false;

        // Reset guard logic
        ChangeRenderMode(false);

        // Hide progress bar
        itemDurationBar.gameObject.SetActive(false);
    }

    private void ChangeRenderMode(bool transparent) {

        

        if (transparent) {
            GetComponent<Renderer>().material = transparentMat;

            // Start the smooth transition to 50% alpha
            StartCoroutine(ChangeAlphaOverTime(transparentMat, 0.5f, 1f)); // 1 second duration
        } else {
            // Start the smooth transition to 100% alpha
            StartCoroutine(ChangeAlphaOverTime(transparentMat, 1f, 1f)); // 1 second duration
            
        }
    }

    private IEnumerator ChangeAlphaOverTime(Material material, float targetAlpha, float duration) {
        Color initialColor = material.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);
        
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, targetAlpha, elapsedTime / duration);
            material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null; // Wait for the next frame
        }

        // Do not set opaque until after the duration
        if (!isUsingDior) {
            GetComponent<Renderer>().material = opaqueMat;
        }
        // Ensure the final alpha value is set
        material.color = targetColor;
    }


}