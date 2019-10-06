using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private GameObject gameLightInner;
    [SerializeField] private GameObject gameLightOuter;
    [SerializeField] private GameObject gameLightDistant;
    private Collider colliderComp;
    
    [SerializeField] private GameObject firefly;
    [SerializeField] private GameObject enemy;

    public static int capacity;
    public static int numFireflies;

    private static float lightRange = 0.025f;
    private float lightTrans = lightRange / 5;

    // Start is called before the first frame update
    void Start()
    {
        capacity = 10;
        numFireflies = 0;
       
        gameLightInner.transform.SetPositionAndRotation(new Vector3(0, 3, 0), new Quaternion(0, 0, 0, 0));
        gameLightOuter.transform.SetPositionAndRotation(new Vector3(0, 3, 0), new Quaternion(0, 0, 0, 0));
        gameLightDistant.transform.SetPositionAndRotation(new Vector3(0, 11, 0), new Quaternion(0, 0, 0, 0));

        gameLightInner.GetComponent<Light>().range = 4;
        gameLightOuter.GetComponent<Light>().range = 7;
        gameLightDistant.GetComponent<Light>().range = 33;

        gameLightInner.GetComponent<Light>().intensity = 1.5f;
        gameLightOuter.GetComponent<Light>().intensity = 0.8f;
        gameLightDistant.GetComponent<Light>().intensity = 0.45f;

        gameLightInner.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.2941177f);
        gameLightOuter.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.2941177f);
        gameLightDistant.GetComponent<Light>().color = new Color(0.308528f, 0.1023941f, 0.5566038f);

        gameLightInner.GetComponent<Light>().shadows = LightShadows.Soft;
        gameLightOuter.GetComponent<Light>().shadows = LightShadows.Soft;
        gameLightDistant.GetComponent<Light>().shadows = LightShadows.None;

        colliderComp = GetComponent<Collider>();
        // Set color and position

        for (int i = 0; i < 5; i++)
        {
            Instantiate(firefly, new Vector3((float)(i * 2 + i ^ 2 / 4), 0.5f, (float)Mathf.Sqrt((i - 2) * (i - 1)) + 2 * i + i / 2 - 4), new Quaternion(0f, 0f, 0f, 0f));
        }
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemy, new Vector3((float)Mathf.Sqrt(Mathf.Abs((i - 2)) * (2 * i)) - 2, 0.5f, (float)(-i / 2) + 2 * i), new Quaternion(0f, 0f, 0f, 0f));
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
        StartCoroutine(SmoothLight(-lightRange));
    }

    IEnumerator SmoothLight(float range)
    {
        numFireflies++;
        Light lInner = gameLightInner.GetComponent<Light>();
        Light lOuter = gameLightOuter.GetComponent<Light>();
        float time = Time.time;
        while (Time.time - time < 0.5f)
        {
            lInner.range += range;
            lInner.transform.Translate(new Vector3(0, (lightTrans * range / Mathf.Abs(range)), 0));
            lOuter.GetComponent<Light>().range += (3f * range);
            lOuter.transform.Translate(new Vector3(0, (3 * lightTrans * range / Mathf.Abs(range)), 0));
            yield return null;
        }
    }
}
