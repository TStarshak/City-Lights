using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireflyCounterForest : MonoBehaviour
{
    private Text display;
    private MissionHandler.Mission currentMission;

    // Start is called before the first frame update
    void Start()
    {
        display = gameObject.GetComponent<Text>();
        currentMission = MissionHandler.Instance.currentMission;
    }

    // Update is called once per frame
    void Update()
    {
        display.text = $"Fireflies Collected: {PlayerState.localPlayerData.firefliesCollected} / {currentMission.fireflyGoal}";
    }
}
