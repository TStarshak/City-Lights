using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public void exitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneController.LoadScene(sceneName);
    }

    public void LoadMainMenu(){
        SceneController.LoadMainMenu();
    }

    public void StartGame(){
        SceneController.LoadFirstScene();
    }
}
