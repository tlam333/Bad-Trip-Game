using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntoxicationManager : MonoBehaviour
    {
    [SerializeField] Slider slider;          // Reference to the Slider component
    [SerializeField] Image fillImage;
    [SerializeField] Color lowColor = Color.green; // Color for low intoxication
    [SerializeField] Color highColor = Color.red;  // Color for high intoxication

    [SerializeField] float currIntoxication = 0;
    [SerializeField] float minIntoxication = 0; // Minimum value for intoxication
    [SerializeField] float maxIntoxication = 1000; // Maximum value for intoxication
    [SerializeField] float transitionDuration = 0.5f;

    void Start() {

        // Initialize the slider's min and max values
        slider.minValue = minIntoxication;
        slider.maxValue = maxIntoxication;
        slider.value = currIntoxication;

        // Initialize the color based on the starting slider value
        UpdateFillColor(slider.value);

        // Add listener to the slider to handle value changes
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    IEnumerator SmoothSliderTransition(float targetValue) {
        float startValue = slider.value;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            slider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = targetValue; // Ensure the slider ends exactly at the target value
        UpdateFillColor(targetValue);
    }

    void OnSliderValueChanged(float value) {
        // Update the color based on the slider value
        UpdateFillColor(value);
    }

    void UpdateFillColor(float value) {
        // Interpolate between lowColor and highColor
        fillImage.color = Color.Lerp(lowColor, highColor, (value - minIntoxication) / (maxIntoxication - minIntoxication));
    }

    public void AddIntoxication() {
        currIntoxication += 25;

        currIntoxication = Mathf.Clamp(currIntoxication, minIntoxication, maxIntoxication);

        StartCoroutine(SmoothSliderTransition(currIntoxication));
    }
}
