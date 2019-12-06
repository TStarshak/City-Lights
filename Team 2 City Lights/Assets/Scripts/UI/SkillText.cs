using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillText : MonoBehaviour
{
    private Text display;
    private PlayerUpgrades currentSkills;
    private PlayerUpgrades.Skill playerSpeed;
    private PlayerUpgrades.Skill vaculampCapacity;
    private PlayerUpgrades.Skill vaculampRange;

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Text>();
        currentSkills = PlayerState.currentUpgrades;
        playerSpeed = currentSkills.skillByName("speed");
        vaculampCapacity = currentSkills.skillByName("vlcapacity");
        vaculampRange = currentSkills.skillByName("vlrange");
    }

    // Update is called once per frame
    void Update()
    {
        //Display info about the player's currently upgraded skills
        if (PauseController.isPaused){
            display.text = $"Speed: lvl {playerSpeed.getCurrentLevel()} | Capacity: lvl {vaculampCapacity.getCurrentLevel()} | Range: lvl {vaculampRange.getCurrentLevel()}";
        }
    }
}
