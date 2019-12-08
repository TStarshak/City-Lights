using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statistics
    public static PlayerData localPlayerData;

    // References global status of player upgrades
    public static PlayerUpgrades currentUpgrades = new PlayerUpgrades();

    private MissionHandler.Mission currentMission;

    private bool hasDied = false;


    // At start, load data from PlayerProgress
<<<<<<< HEAD
    void Awake()
    {
        hasDied = false;
=======
    void OnEnable(){
>>>>>>> 56f1af73173aaf9d22bdefcc464d015ca651e53e
        localPlayerData = new PlayerData(PlayerProgress.Instance.savedPlayerData);
    }

    void Start()
    {
        hasDied = false;
        currentMission = MissionHandler.Instance.currentMission;
    }

    void Update()
    {
        // Check the player's status in accomplishing their mission
        if (localPlayerData.firefliesCollected >= currentMission.fireflyGoal)
        {
            MissionHandler.Instance.updateMissionStatus(true);
        }
        else if (localPlayerData.firefliesCollected < currentMission.fireflyGoal)
        {
            MissionHandler.Instance.updateMissionStatus(false);
        }
        if (localPlayerData.isDead && !hasDied)
        {
            hasDied = true;
            StartCoroutine(Death());

        }
    }

    // Save Data to Global Player Progress
    public static void SavePlayer()
    {
        PlayerProgress.Instance.savedPlayerData = new PlayerData(localPlayerData);
        PlayerProgress.Instance.currentUpgrades = currentUpgrades;
    }

    public static void applyMultipliers()
    {
        localPlayerData.movementSpeed *= currentUpgrades.skillByName("speed").getMultiplier();
        localPlayerData.vacuLampCapacity *= currentUpgrades.skillByName("vlcapacity").getMultiplier();
        localPlayerData.vacuLampRange = currentUpgrades.skillByName("vlrange").getMultiplier();
    }

    public static void dangerState()
    {
        if (localPlayerData.inDangerState)
        {
            localPlayerData.isDead = true;
        }
        else
        {
            localPlayerData.inDangerState = true;
            //Camera Shake

            //Turn Edge of player red
        }
    }

    IEnumerator Death()
    {
        Animator anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim.SetTrigger("Death");
        //After playing the animation, move to the city
        Debug.Log("DEATH");

        yield return new WaitForSeconds(3f);

        SceneController.LoadScene("City");
        yield return null;
    }
}
