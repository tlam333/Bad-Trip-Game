using System.Collections;
using UnityEngine;
using TMPro;

public class TriggerScreenText : MonoBehaviour
{
    public TextMeshProUGUI triggerText; // Reference to the TextMeshProUGUI component
    public string message = "You entered the trigger!"; // The message to display
    public float maxDelay = 1f; // Delay between each word
    public float minDelay = 0.1f;
    public float remainDuration = 2f; // Duration to keep the text visible after all words are displayed
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip sound; // The sound clip to play when the trigger is entered

    private Coroutine displayTextCoroutine;
    private System.Random random = new System.Random();

    void Start()
    {
        // Hide the text at the start
        if (triggerText != null)
        {
            triggerText.text = "";
        }

        // If the AudioSource is not assigned, try to get it from the same GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Display the message and play the sound when an object enters the trigger
        if (triggerText != null && other.gameObject.CompareTag("Player"))
        {

            // Stop any existing coroutine to avoid multiple coroutines running simultaneously
            if (displayTextCoroutine != null)
            {
                StopCoroutine(displayTextCoroutine);
            }

            // Start a coroutine to display the text one word at a time
            displayTextCoroutine = StartCoroutine(DisplayTextOneWordAtATime());
        }
    }

    IEnumerator DisplayTextOneWordAtATime()
    {
        // Split the message into words
        string[] words = message.Split(' ');
        triggerText.text = "";

        // Display each word with a delay
        foreach (string word in words)
        {
            // Play the sound if an audio source and clip are assigned
            if (audioSource != null && sound != null)
            {
                audioSource.PlayOneShot(sound);
                Debug.Log("Played Sound");
            }

            triggerText.text += word + " "; // Add the word and a space to the text

            float displayDelay = (float)random.NextDouble() * (maxDelay - minDelay) + minDelay;

            yield return new WaitForSeconds(displayDelay); // Wait for the delay before showing the next word
        }

        // Wait for the displayDuration after all words are shown
        yield return new WaitForSeconds(remainDuration);

        // Clear the text after the delay
        triggerText.text = "";
    }
}
