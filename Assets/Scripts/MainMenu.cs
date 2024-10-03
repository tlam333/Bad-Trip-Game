using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scenename;

    public void PlayGame() {
        SceneManager.LoadScene(scenename);
    }

    public void QuitGame() {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
