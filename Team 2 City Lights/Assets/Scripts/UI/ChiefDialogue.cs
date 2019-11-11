using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiefDialogue : MonoBehaviour
{
    [SerializeField] private Text dialogue; // The text that will be displayed in the speech bubble
    [SerializeField] private GameObject dialogueCanvas; // The dialogue bubble of the chief
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        setupDialogue();
        hideDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // Show the chief's speech bubble when the player is near
        // Alternatively, this can be done with a collider (stretch goal)
        if (player.transform.position.x < -4){
            displayDialogue();
        }
        else{
            hideDialogue();
        }
    }

    void setupDialogue(){
        dialogue.text = $"We need you to collect <color=#FBE92B><size=20><b><i> {MissionHandler.Instance.currentMission.fireflyGoal}</i></b></size></color> fireflies! Good luck out there, kid!";
    }

    void displayDialogue(){
        dialogueCanvas.SetActive(true);
    }

    void hideDialogue(){
        dialogueCanvas.SetActive(false);
    }
}
