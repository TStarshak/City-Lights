using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeFramework
{
    //Hardcoded list of possible upgrades for the game
    private List<string> possibleUpgrades = new List<string>{"speed", "vlrange", "vlcapacity"};

    private List<string> upgradeTitles;
    private List<int> upgradeLevels;

    /*
     * Instantiate an upgrade framework without any existing upgrades
     * which need to be greater than level 0
     */
    public upgradeFramework()
    {
        upgradeTitles = new List<string>();
        upgradeLevels = new List<int>();
    }

    /*
     * Instantiate an upgrade framework with the intention of starting
     * some upgrades at a level greater than 0
     */
    public upgradeFramework(List<string> upgrades, List<int> levels)
    {
        upgradeTitles = upgrades;
        upgradeLevels = levels;
    }

    /*
     * Adjust the levels that specific upgrades are at
     */
    public void adjustUpgrades(List<string> upgrades, List<int> levels)
    {
        for(int i = 0; i < upgrades.Count; i++)
        {
            if (possibleUpgrades.Contains(upgrades[i]))
            {
                if (upgradeTitles.IndexOf(upgrades[i]) != -1)
                {
                    upgradeLevels[upgradeTitles.IndexOf(upgrades[i])] = levels[i];
                } else
                {
                    upgradeTitles.Add(upgrades[i]);
                    upgradeLevels.Add(levels[i]);
                }
                
            }
        }
    }

    /*
     * Returns the level that specific upgrades are at
     */
    public List<int> getUpgradeLevels(List<string> upgrades)
    {
        List<int> levels = new List<int>();
        foreach (string s in upgrades)
        {
            if (upgradeTitles.Contains(s))
            {
                levels.Add(upgradeLevels[upgradeTitles.IndexOf(s)]);
            } else
            {
                levels.Add(-1);
            }
        }

        return levels;
    }

    /*
     * Returns the numerical values that we will be directly placed into variables
     * for their respective action
     */
    public List<float> getUpgradeValues(List<string> upgrades)
    {
        List<float> values = new List<float>();
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgradeTitles.Contains(upgrades[i]))
            {
                //Cases based on possibleUpgrades List
                switch (upgrades[i])
                {
                    case "speed":
                        //PUT FORMULA FOR SPEED VALUE HERE 
                        values.Add(1);
                        break;

                    case "vlrange":
                        //PUT FORMULA FOR VACU-LAMP RANGE VALUE HERE 
                        values.Add(1);
                        break;

                    case "vlcapacity":
                        //PUT FORMULA FOR VACU-LAMP CAPACITY VALUE HERE 
                        values.Add(1);
                        break;
                }
            } else
            {
                values.Add(-1);
            }
        }

        return values;
    }
}
