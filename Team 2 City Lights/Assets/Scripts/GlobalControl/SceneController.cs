using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Global Script for Scene Management and Navigation */
public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    // Called upon object creation
    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {

    }

    public static void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
