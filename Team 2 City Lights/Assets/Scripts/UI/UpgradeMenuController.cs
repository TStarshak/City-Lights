using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuController : MonoBehaviour
{

    // Button Declarations
    [SerializeField] private Button exitButton; // Button to exit the menu
    [SerializeField] private Button capacityUpgradeButton; // Button to upgrade vaculamp capacity
    [SerializeField] private Button speedUpgradeButton; // Button to upgrade player speed

    // Skill Declarations
    private PlayerUpgrades.Skill vaculampCapacity;
    private PlayerUpgrades.Skill playerSpeed;
    private PlayerUpgrades.Skill vaculampRange;

    // Text declarations
    private Text capacityUpgradePrice;
    private Text speedUpgradePrice;


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
        speedUpgradeButton.onClick.AddListener(UpgradeSpeed);

        //Set Price for Upgrades
        capacityUpgradePrice = capacityUpgradeButton.GetComponentInChildren<Text>();
        capacityUpgradePrice.text = $"{vaculampCapacity.getUpgradeCost()} fireflies";
        speedUpgradePrice = capacityUpgradeButton.GetComponentInChildren<Text>();
        speedUpgradePrice.text = $"{playerSpeed.getUpgradeCost()} fireflies";

        //Set Price for Speed
        speedUpgradePrice = speedUpgradeButton.GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        ButtonHandler(capacityUpgradeButton, vaculampCapacity, capacityUpgradePrice);
        ButtonHandler(speedUpgradeButton, playerSpeed, speedUpgradePrice);
    }

    void ResumeGame(){
        gameObject.SetActive(false);
        PauseController.enablePauseFunctionality();
    }

    // Manages the enabling/disabling and the text content of a button and its associated skill
    void ButtonHandler(Button upgradeButton, PlayerUpgrades.Skill skill, Text priceText){
        if (Economy.CanAffordSkill(PlayerState.localPlayerData, skill)){
            // Enable button and show purchasable price color
            upgradeButton.interactable = true;
            priceText.color = new Color(255, 243, 42);
        }
        else {
            if (skill.isUpgradeable()){
                // Disable button and change text color if unaffordable
                priceText.color = new Color(250, 84, 82);
                upgradeButton.interactable = false;
            }
            else {
                // Skill is maxed and does not have a cost
                upgradeButton.interactable = false;
                priceText.color = new Color(255, 243, 42);
                capacityUpgradePrice.text = "Skill is Maxed";
            }
        }
    }

    void UpgradeCapacity(){
        UpgradeSkill(vaculampCapacity, capacityUpgradePrice);
    }

    void UpgradeSpeed(){
        UpgradeSkill(playerSpeed, speedUpgradePrice);
    }

    // Upgrades the player's given skill and displayed price
    private void UpgradeSkill(PlayerUpgrades.Skill skill, Text priceText){
        PurchaseUpgrade(skill);
        priceText.text = $"{skill.getUpgradeCost()} fireflies";

    }

    // Purchases a provided upgrade
    private void PurchaseUpgrade(PlayerUpgrades.Skill skillUpgrade){
        Economy.PurchaseSkill(PlayerState.localPlayerData, skillUpgrade);
        PlayerState.applyMultipliers();
    }
    
    // // Manages the enabling/disabling and the text content of the button
    // private void capacityButtonHandler(){
    //     if (Economy.CanAffordSkill(PlayerState.localPlayerData, vaculampCapacity)){
    //         // Enable button and show purchasable price color
    //         capacityUpgradeButton.interactable = true;
    //         capacityUpgradePrice.color = new Color(255, 243, 42);

    //     }
    //     else {
    //         if (vaculampCapacity.isUpgradeable()){
    //             // Disable button and change text color if unaffordable
    //             capacityUpgradePrice.color = new Color(250, 84, 82);
    //             capacityUpgradeButton.interactable = false;
    //         }
    //         else {
    //             // Skill is maxed and does not have a cost
    //             capacityUpgradeButton.interactable = false;
    //             capacityUpgradePrice.color = new Color(255, 243, 42);
    //             capacityUpgradePrice.text = "Skill is Maxed";
    //         }
            
    //     }
    // }
}
