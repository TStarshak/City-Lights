using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades
{
    // Hardcoded list of default possible upgrades for the game
    private List<string> possibleUpgrades = new List<string>{"speed", "vlrange", "vlcapacity"};

    // The list of upgradeable player skills
    private List<Skill> skills;

    /* An upgradeable skill of the player */
    public class Skill{
        private string name;
        private float multiplier;
        private int costToUpgrade;
        private int currentLevel;
        private int maxLevel;
        private bool isMaxed;

        // Create a new player skill with default beginning values
        public Skill(string name){
            this.name = name;
            this.multiplier = 1.0f;
            this.costToUpgrade = 10;
            this.currentLevel = 0;
            this.maxLevel = 3;
            this.isMaxed = false;
        }

        public string getName(){
            return name;
        }

        public float getMultiplier(){
            return multiplier;
        }

        public int getUpgradeCost(){
            return costToUpgrade;
        }

        public int getCurrentLevel(){
            return currentLevel;
        }

        public int getMaxLevel(){
            return maxLevel;
        }

        /* Whether this skill can be upgraded. If not, then it's maxed out */
        public bool isUpgradeable(){
            return !isMaxed;
        }

        /* Upgrade this skill to the next level (if available)
           Returns true/false whether the upgrade was successful or not */
        public bool upgradeLevel(){
            if (isUpgradeable()){
                multiplier += 0.5f;
                costToUpgrade += 10;
                currentLevel++;
                isMaxed = currentLevel >= maxLevel;    // If the level reaches 3, this skill is maxed
                return true; // Return true; skill has been upgrade
            }
            return false;   // Return false; upgrade is maxed
        }

    }

    /* Upgrades (Default)
     * Instantiate an upgrade framework with default hardcoded possible upgrades
     */
    public PlayerUpgrades()
    {
        // Create list of skills based off of the default possible upgrades
        skills = new List<Skill>();
        possibleUpgrades.ForEach(upgrade => { skills.Add(new Skill(upgrade)); });
    }

    /* Upgrades (Custom)
     * Instantiate an upgrade framework with the intention of providing custom skills to the player
     */
    public PlayerUpgrades(List<Skill> upgrades)
    {
        // Assign the list of skills to the provided custom list
        skills = upgrades;
    }

    public List<Skill> GetSkills(){
        return skills;
    }

    /* Retrieve a skill by name from the list of skills
     * Returns null if the skill isn't found
     */
    public Skill skillByName(string skillName){
        Skill targetSkill = null;
        skills.ForEach(skill => { 
            if(skill.getName() == skillName) targetSkill = skill; 
        });
        return targetSkill;
    }

    /* Retrieve the average skill level of the player
    */
    public int highestSkillLevel(){
        int highestLevel = skills[0].getCurrentLevel();
        foreach(Skill skill in skills){
           if (skill.getCurrentLevel() > highestLevel) { highestLevel = skill.getCurrentLevel();}
        }
        return highestLevel;
    }

    // }
}

            /*************** Deprecated/Unused from Original Framework ******************************/
    // /*
    //  * Adjust the levels that specific upgrades are at
    //  */
    // public void adjustUpgrades(List<string> upgrades, List<int> levels)
    // {
    //     for(int i = 0; i < upgrades.Count; i++)
    //     {
    //         if (possibleUpgrades.Contains(upgrades[i]))
    //         {
    //             if (upgradeTitles.IndexOf(upgrades[i]) != -1)
    //             {
    //                 upgradeLevels[upgradeTitles.IndexOf(upgrades[i])] = levels[i];
    //             } else
    //             {
    //                 upgradeTitles.Add(upgrades[i]);
    //                 upgradeLevels.Add(levels[i]);
    //             }
                
    //         }
    //     }
    // }

    // /*
    //  * Returns the level that specific upgrades are at
    //  */
    // public List<int> getUpgradeLevels(List<string> upgrades)
    // {
    //     List<int> levels = new List<int>();
    //     foreach (string s in upgrades)
    //     {
    //         if (upgradeTitles.Contains(s))
    //         {
    //             levels.Add(upgradeLevels[upgradeTitles.IndexOf(s)]);
    //         } else
    //         {
    //             levels.Add(-1);
    //         }
    //     }

    //     return levels;
    // }

    // /*
    //  * Returns the numerical values that we will be directly placed into variables
    //  * for their respective action
    //  */
    // public List<float> getUpgradeValues(List<string> upgrades)
    // {
    //     List<float> values = new List<float>();
    //     for (int i = 0; i < upgrades.Count; i++)
    //     {
    //         if (upgradeTitles.Contains(upgrades[i]))
    //         {
    //             //Cases based on possibleUpgrades List
    //             switch (upgrades[i])
    //             {
    //                 case "speed":
    //                     //PUT FORMULA FOR SPEED VALUE HERE 
    //                     values.Add(1);
    //                     break;

    //                 case "vlrange":
    //                     //PUT FORMULA FOR VACU-LAMP RANGE VALUE HERE 
    //                     values.Add(1);
    //                     break;

    //                 case "vlcapacity":
    //                     //PUT FORMULA FOR VACU-LAMP CAPACITY VALUE HERE 
    //                     values.Add(1);
    //                     break;
    //             }
    //         } else
    //         {
    //             values.Add(-1);
    //         }
    //     }

    //     return values;
    // }

