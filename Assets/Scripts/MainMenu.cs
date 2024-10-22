using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import UI for Slider

public class MainMenu : MonoBehaviour
{
    public string scenename;
    public GameObject sceneTransitionPrefab;

    // Slider for controlling the player movement speed
    public Slider speedSlider;  // Drag the slider from the UI here

    // A static variable to store the player speed, accessible across scenes
    public static float playerSpeed = 3f;  // Default speed value

    public void PlayGame() {
        // Save the slider value to the playerSpeed variable
        playerSpeed = speedSlider.value;

        // Trigger the scene transition
        TriggerSceneTransition();
    }

    public void QuitGame() {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void TriggerSceneTransition()
    {
        // Instantiate the scene transition prefab
        GameObject transition = Instantiate(sceneTransitionPrefab);

        // Get the SceneTransition component and set the scene name to transition to
        SceneTransition sceneTransition = transition.GetComponent<SceneTransition>();
        sceneTransition.nextSceneName = scenename; // Assign the next scene name

        // The transition will start automatically once instantiated
    }
}
