using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public string scenename;
 
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Scene Name: " + scenename);
            SceneManager.LoadScene(scenename);
        }
    }
}