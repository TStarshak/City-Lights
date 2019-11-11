using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{

    public static MissionHandler Instance;
    public Mission currentMission;


    // A subclass for a mission to be assigned by the Fireflyter Chief
    public class Mission{

        public int fireflyGoal;
        public bool hasMetGoal;

        public Mission(int newGoal){
            // Set the Default firefly goal
            fireflyGoal = newGoal;
            // Tracks whether the player has met thee goal for this mission
            hasMetGoal = false;
        }

    }

    // Start is called before the first frame update
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

        currentMission = new Mission(10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateMissionStatus(bool isGoalMet){
        currentMission.hasMetGoal = isGoalMet;
    }

    public void resetMission(){
        currentMission = new Mission(currentMission.fireflyGoal);
    }


}
