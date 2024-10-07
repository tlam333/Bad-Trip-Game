using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI; // For Image component

public class SceneTransition : MonoBehaviour
{
    public Image blackScreen; // Reference to black screen image
    public TextMeshProUGUI transitionText; // Reference to TextMeshPro text
    public float fadeDuration = 2f;
    public string nextSceneName;

    private Color blackScreenColor;
    private Color textColor;

    private void Start()
    {
        // Initialize colors and set alpha to 0 (fully transparent at start)
        blackScreenColor = blackScreen.color;
        textColor = transitionText.color;

        blackScreenColor.a = 0f;
        textColor.a = 0f;

        blackScreen.color = blackScreenColor;
        transitionText.color = textColor;

        StartCoroutine(FadeOutAndLoadScene());
    }

    public IEnumerator FadeOutAndLoadScene()
    {
        float fadeSpeed = 2f / fadeDuration;
        float progress = 0f;

        // Fade in the black screen and text
        while (progress < 3f)
        {
            progress += Time.deltaTime * fadeSpeed;
            blackScreenColor.a = Mathf.Clamp01(progress);
            textColor.a = Mathf.Clamp01(progress);

            blackScreen.color = blackScreenColor;
            transitionText.color = textColor;

            yield return null;
        }

        // After fade-in, load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    public void SetTransitionText(string text)
    {
        if (transitionText != null)
        {
            transitionText.text = text; // Update the TextMeshPro text
        }
    }
}