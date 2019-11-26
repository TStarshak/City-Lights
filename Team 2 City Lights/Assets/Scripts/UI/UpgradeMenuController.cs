using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuController : MonoBehaviour
{

    [SerializeField] private Button exitButton; // Button to exit the menu
    [SerializeField] private Button capacityUpgradeButton; // Button to upgrade vaculamp capacity

    private PlayerUpgrades.Skill vaculampCapacity;
    private PlayerUpgrades.Skill playerSpeed;
    private PlayerUpgrades.Skill vaculampRange;

    private Text capacityUpgradePrice;


    // Start is called before the first frame update
    void Start()
    {
        //Map appropriate skills to their variables
        vaculampCapacity = PlayerState.currentUpgrades.skillByName("vlcapacity");
        vaculampRange = PlayerState.currentUpgrades.skillByName("vlrange");
        playerSpeed = PlayerState.currentUpgrades.skillByName("speed");

        //Listeners for buttons
        exitButton.onClick.AddListener(ResumeGame);
        capacityUpgradeButton.onClick.AddListener(UpgradeCapacity);


        //Set Price for Upgrades
        capacityUpgradePrice = capacityUpgradeButton.GetComponentInChildren<Text>();
        capacityUpgradePrice.text = $"{vaculampCapacity.getUpgradeCost()} fireflies";

    }

    // Update is called once per frame
    void Update()
    {
        capacityButtonHandler();
    }

    void ResumeGame(){
        gameObject.SetActive(false);
        PauseController.enablePauseFunctionality();
    }

    // Upgrades the player's capacity
    void UpgradeCapacity(){
        PurchaseUpgrade(vaculampCapacity);
        capacityUpgradePrice.text = $"{vaculampCapacity.getUpgradeCost()} fireflies";

    }

    // Purchases a provided upgrade
    private void PurchaseUpgrade(PlayerUpgrades.Skill skillUpgrade){
        Economy.PurchaseSkill(PlayerState.localPlayerData, skillUpgrade);
        PlayerState.applyMultipliers();
    }

    // Manages the enabling/disabling and the text content of the button
    private void capacityButtonHandler(){
        if (Economy.CanAffordSkill(PlayerState.localPlayerData, vaculampCapacity)){
            // Enable button and show purchasable price color
            capacityUpgradeButton.interactable = true;
            capacityUpgradePrice.color = new Color(255, 243, 42);

        }
        else {
            if (vaculampCapacity.isUpgradeable()){
                // Disable button and change text color if unaffordable
                capacityUpgradePrice.color = new Color(250, 84, 82);
                capacityUpgradeButton.interactable = false;
            }
            else {
                // Skill is maxed and does not have a cost
                capacityUpgradeButton.interactable = false;
                capacityUpgradePrice.color = new Color(255, 243, 42);
                capacityUpgradePrice.text = "Skill is Maxed";
            }
            
        }
    }
}
