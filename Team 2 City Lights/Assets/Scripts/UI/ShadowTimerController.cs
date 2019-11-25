using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTimerController : MonoBehaviour
{
    public static bool shadowHour = false;     // Whether the timer has expired. Shades come out when true.

    [SerializeField]
    private int timerStart = 180;
    private int timeUntilShadowHour;
    public GameObject shadowIcon;
    public GameObject fireflyAnimation;
    public GameObject ring1;
    public GameObject ring2;
    // Start is called before the first frame update
    void Start()
    {
        //RotateTimer();
        Time.timeScale = 1;
        timeUntilShadowHour = timerStart;
        StartCoroutine("Countdown");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (pauseM.activeInHierarchy == false)
        {
            this.transform.Rotate(0, 0, 0.37f, Space.Self);
        }*/

        if (shadowHour)
        {
            this.gameObject.SetActive(false);
            shadowIcon.SetActive(true);
            fireflyAnimation.SetActive(false);
            ring1.SetActive(false);
            ring2.SetActive(true);
        }
    }
    /*
        IEnumerator RotateTimer()
        {
            while (true)
            {
                if (pauseM.activeInHierarchy == false)
                {
                    yield return new WaitForSeconds(1);
                    transform.Rotate(0, 0, 5, Space.Self);
                }
            }
        }*/

    IEnumerator Countdown()
    {
        // Reduce the time until Shadow Hour
        while (timeUntilShadowHour > 0)
        {
            yield return new WaitForSeconds(1);
            if (PauseController.isPaused == false)
            {
                timeUntilShadowHour--;
                this.transform.Rotate(0, 0, -2, Space.Self);
            }
        }
        // Enable Shadow Hour when the time has reached 0
        shadowHour = true;
    }
}
