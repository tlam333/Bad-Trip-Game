using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneTransit : MonoBehaviour
{
    public string scenename;
    public GameObject sceneTransitionPrefab; // Reference to the SceneTransition prefab

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the transition instead of directly loading the scene
            TriggerSceneTransition();
        }
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
