using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private GameObject gameLightInner;
    [SerializeField] private GameObject gameLightOuter;
    [SerializeField] private GameObject gameLightPlayer;
    private Collider colliderComp;
    
    [SerializeField] private GameObject firefly;
    [SerializeField] private GameObject enemy;

    public static int capacity;
    public static int numFireflies;

    private static float lightRange = 0.02f;
    private float lightTrans = lightRange / 5;

    // Start is called before the first frame update
    void Start()
    {
        capacity = 10;
        numFireflies = 0;
       
        gameLightInner.transform.SetPositionAndRotation(new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));
        gameLightOuter.transform.SetPositionAndRotation(new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));

        gameLightInner.GetComponent<Light>().range = 4;
        gameLightOuter.GetComponent<Light>().range = 7;

        gameLightInner.GetComponent<Light>().intensity = 1.5f;
        gameLightOuter.GetComponent<Light>().intensity = 0.8f;
        gameLightPlayer.GetComponent<Light>().intensity = 1f;

        gameLightInner.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.2941177f);
        gameLightOuter.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.2941177f);
        gameLightPlayer.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.4941177f);

        gameLightInner.GetComponent<Light>().shadows = LightShadows.Soft;
        gameLightOuter.GetComponent<Light>().shadows = LightShadows.Soft;
        gameLightPlayer.GetComponent<Light>().shadows = LightShadows.Soft;

        colliderComp = GetComponent<Collider>();
        // Set color and position

        GameObject fly;
        for (int i = 0; i < 5; i++)
        {
            fly = Instantiate(firefly, new Vector3((float)(i * 2 + i ^ 2 / 4), 0.5f, (float)Mathf.Sqrt((i - 2) * (i - 1)) + 2 * i + i / 2 - 4), new Quaternion(0f, 0f, 0f, 0f));
            fly.GetComponent<Animator>().SetFloat("Offset", Random.value * 1.5f);
            fly.GetComponent<Animator>().speed = Random.value * 1.5f + 0.1f;
        }
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemy, new Vector3((float)Mathf.Sqrt(Mathf.Abs((i - 2)) * (2 * i)) - 2, 1.25f, (float)(-i / 2) + 2 * i), new Quaternion(0f, 0f, 0f, 0f));
        }
    }
    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCoroutine(SmoothLight(lightRange));
        if (Input.GetKeyDown(KeyCode.R) && gameLightInner.GetComponent<Light>().range > 3)
            StartCoroutine(SmoothLight(-lightRange)); 
    }

    void onFireflyEnter()
    {
        StartCoroutine(SmoothLight(lightRange));
    }

    void onEnemyEnter()
    {
        if(gameLightInner.GetComponent<Light>().range > 3)
         StartCoroutine(SmoothLight(-lightRange));
    }

    IEnumerator SmoothLight(float range)
    {
        numFireflies++;
        Light lInner = gameLightInner.GetComponent<Light>();
        Light lOuter = gameLightOuter.GetComponent<Light>();
        Light lPlayer = gameLightPlayer.GetComponent<Light>();
        float time = Time.time;
        if((lPlayer.intensity > 1 && range < 0) || (lPlayer.intensity > 3 && range > 0))
            lPlayer.intensity += (0.2f * Mathf.Abs(range) / range);
        while (Time.time - time < 0.5f)
        {
            lInner.range += range;
            lInner.transform.Translate(new Vector3(0, (lightTrans * range / Mathf.Abs(range)), 0));
            lOuter.range += (1.5f * range);
            lOuter.transform.Translate(new Vector3(0, (1.5f * lightTrans * range / Mathf.Abs(range)), 0));
            yield return null;
        }
    }
}
