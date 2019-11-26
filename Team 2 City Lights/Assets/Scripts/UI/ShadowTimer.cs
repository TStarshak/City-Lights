using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowTimer : MonoBehaviour
{
    public bool shadowHour = false;     // Whether the timer has expired. Shades come out when true.
    public Text countDisplay;           // Text object to show the countdown in the UI

    [SerializeField]
    private int timerStart = 90;
    private int timeUntilShadowHour;         // The amount of time left in seconds

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        timeUntilShadowHour = timerStart;
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        // Reduce the time until Shadow Hour
        while (timeUntilShadowHour > 0) {
            yield return new WaitForSeconds(1);
            timeUntilShadowHour--;
            //Display the timer if not expired (minutes and seconds)
            countDisplay.text = (minutesLeft() + ":" + secondsLeft().ToString("00")); 
        }
        // Enable Shadow Hour when the time has reached 0
        shadowHour = true;
        countDisplay.text = "Shadow Hour";

    }

    private int minutesLeft()
    {
        return Mathf.FloorToInt(timeUntilShadowHour / 60);
    }

    private int secondsLeft()
    {
        return Mathf.FloorToInt(timeUntilShadowHour % 60);
    }
}
