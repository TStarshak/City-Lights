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
        SceneManager.LoadScene(sceneName);
    }

    //Quit to main menu and reset progress
    public void QuitAndReset(){
        SceneController.Instance.ResetGame();
        LoadScene("MainMenu");
    }
}
