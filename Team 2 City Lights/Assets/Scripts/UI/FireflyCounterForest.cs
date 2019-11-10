using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireflyCounterForest : MonoBehaviour
{
    private Text display;

    // Start is called before the first frame update
    void Start()
    {
        display = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = "Fireflies Collected: " + PlayerState.localPlayerData.firefliesCollected;
    }
}
