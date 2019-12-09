using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroduction : MonoBehaviour
{
    public GameObject fireflyterChief;
    private Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        fireflyterChief.SetActive(false);
        PauseController.disablePauseFunctionality();
        continueButton = GetComponentInChildren<Button>();
        continueButton.onClick.AddListener(StartGame);
    }

    void StartGame(){
        PauseController.enablePauseFunctionality();
        fireflyterChief.SetActive(true);
        gameObject.SetActive(false);
    }
}
