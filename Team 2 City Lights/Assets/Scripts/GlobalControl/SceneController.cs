using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Global Script for Scene Management and Navigation */
public class SceneController : MonoBehaviour
{

    public static SceneController Instance;
    public static Scene currentScene; //The Current Loaded Scene

    // Called upon object creation
    void Awake()
    {
        // Retrieve the currently active scene as the initial scene
        currentScene = SceneManager.GetActiveScene();

        // Instantiate this tracker if none exists yet and keep game object alive
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        // Otherwise prevent duplicates
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

    public static void LoadScene(string scene){
        SceneManager.LoadScene(scene);
        currentScene = SceneManager.GetSceneByName(scene);
    }
}
