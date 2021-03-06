﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Global Script for Scene Management and Navigation */
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public static Scene currentScene;   //The Current Loaded Scene
    
    public Scene previousScene;          // The previously loaded scene

    // Called upon object creation
    void Awake()
    {
        // Retrieve the currently active scene as the initial scene
        currentScene = previousScene = SceneManager.GetActiveScene();
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

    public static void LoadFirstScene(){
        Instance.InitLoadingScene("City");
        currentScene = SceneManager.GetSceneByName("City");
    }

    public static void LoadMainMenu(){
        PlayerState.SavePlayer();
        Instance.InitLoadingScene("MainMenu");
        currentScene = SceneManager.GetSceneByName("MainMenu");
        PlayerProgress.Instance.returningFromForest = false;
    }

    // Load a given scene and automatically save progress
    public static void LoadScene(string scene){
        PlayerState.SavePlayer();
        if (currentScene.name == "PrototypeScene") {PlayerProgress.Instance.returningFromForest = true;}
        Instance.InitLoadingScene(scene);
        currentScene = SceneManager.GetSceneByName(scene);
    }

    public string previousSceneName(){
        return previousScene.name;
    }

    // Displays the loading screen and begins loading the next scene in the background
    void InitLoadingScene(string nextScene){
        SaveSystem.SavePlayer(PlayerProgress.Instance.savedPlayerData, PlayerProgress.Instance.currentUpgrades);
        Instance.previousScene = currentScene;
        Time.timeScale = 1;
        SceneManager.LoadScene("LoadingScreen");
        //Start asyncOperation
        StartCoroutine(LoadAsyncScene(nextScene));
    }

    IEnumerator LoadAsyncScene(string nextScene){
        // SceneManager.UnloadSceneAsync(currentScene);
        yield return new WaitForSeconds(3.0f);  //Buffer for short load times
        // Create an async operation
        SceneManager.LoadScene(nextScene);
        // AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        // loadingLevel.allowSceneActivation = false;
        // while (loadingLevel.progress <= 0.9f){
        //     yield return null;
        // }
        // loadingLevel.allowSceneActivation = true;
        // When finished, load the game scene
        yield return new WaitForEndOfFrame();
    }
}
