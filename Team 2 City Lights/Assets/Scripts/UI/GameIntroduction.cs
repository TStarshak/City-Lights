using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroduction : MonoBehaviour
{
    public GameObject fireflyterChief;
    [SerializeField] private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        fireflyterChief.SetActive(false);
        PauseController.disablePauseFunctionality();
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame(){
        PauseController.enablePauseFunctionality();
        fireflyterChief.SetActive(true);
        gameObject.SetActive(false);
    }
}
