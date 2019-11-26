using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireflyCounterCity : MonoBehaviour
{
    private Text display;
    [SerializeField] private GameObject upgradeMenu;

    // Start is called before the first frame update
    void Start()
    {
        display = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseController.pauseIsEnabled() || upgradeMenu.activeInHierarchy){
            display.text = $"Fireflies in Wallet: {PlayerState.localPlayerData.firefliesInWallet}";
        }
        else {
            display.text = "";
        }
    }
}
