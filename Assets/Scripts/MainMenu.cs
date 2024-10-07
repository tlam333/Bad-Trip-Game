using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scenename;
    public GameObject sceneTransitionPrefab;

    public void PlayGame() {
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
