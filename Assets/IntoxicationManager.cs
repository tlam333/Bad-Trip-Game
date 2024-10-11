using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntoxicationManager : MonoBehaviour
{
    [SerializeField] private Slider intoxicationSlider; // Reference to the UI Slider
    [SerializeField] private float maxIntoxication = 100f; // Max intoxication level
    public float currentIntoxication = 0f; // Current intoxication level

    // Colors for the intoxication bar
    public Color soberColor = Color.green;
    public Color drunkColor = Color.red;
    private Image fillImage;

    [SerializeField] private float transitionTime = 2f; // Speed at which intoxication increases

    private void Start()
    {
        // Initialize the slider
        intoxicationSlider.maxValue = maxIntoxication;
        intoxicationSlider.value = currentIntoxication;

        // Get the Fill Image component of the slider
        fillImage = intoxicationSlider.fillRect.GetComponent<Image>();
        UpdateSliderColor();
    }

    private void Update()
    {
        // Update the slider value and color in real-time
        intoxicationSlider.value = currentIntoxication;
        UpdateSliderColor();
    }

    // Method to change the color of the slider based on intoxication level
    private void UpdateSliderColor()
    {
        float t = currentIntoxication / maxIntoxication; // Calculate how full the slider is
        fillImage.color = Color.Lerp(soberColor, drunkColor, t); // Lerp between colors based on intoxication
    }

    public void AddIntoxication()
    {
        float targetIntoxication = Mathf.Clamp(currentIntoxication + 150f, 0, maxIntoxication); // Ensure we don't exceed max intoxication
        StartCoroutine(SmoothIncreaseIntoxication(targetIntoxication));
    }

    private IEnumerator SmoothIncreaseIntoxication(float targetIntoxication)
    {
        float initialIntoxication = currentIntoxication;

        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            // SmoothStep gradually increases the value with acceleration and deceleration
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionTime);
            currentIntoxication = Mathf.Lerp(initialIntoxication, targetIntoxication, t);
            
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final value is set precisely
        currentIntoxication = targetIntoxication;
    }
}
