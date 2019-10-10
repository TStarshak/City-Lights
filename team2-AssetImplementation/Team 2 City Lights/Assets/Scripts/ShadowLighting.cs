using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLighting : MonoBehaviour
{
    private Light leftEye;
    private Light rightEye;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        finished = true;
        leftEye = transform.GetChild(0).GetComponent<Light>();
        rightEye = transform.GetChild(1).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(finished)
            StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        finished = false;
        float time = Time.time;
        while(Time.time - time < 1)
        {
            leftEye.intensity += 0.01f;
            rightEye.intensity += 0.01f;
        }
        yield return new WaitForSeconds(0.5f);
        time = Time.time;
        while (Time.time - time < 1)
        {
            leftEye.intensity -= 0.01f;
            rightEye.intensity -= 0.01f;
        }
        yield return new WaitForSeconds(0.5f);
        finished = true;
        yield return null;
    }


}
