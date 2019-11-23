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

    public void StartGame(){
        SceneController.LoadFirstScene();
    }

    //Quit to main menu and reset progress
    public void QuitAndReset(){
        SceneController.Instance.ResetGame();
        LoadScene("MainMenu");
    }
}
