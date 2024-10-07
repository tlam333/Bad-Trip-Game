using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickScene : MonoBehaviour
{
    public string scenename;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the transition instead of directly loading the scene
            SceneManager.LoadScene(scenename);
        }
    }

}
