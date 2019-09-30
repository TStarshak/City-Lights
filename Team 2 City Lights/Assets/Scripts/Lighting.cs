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

    public int capacity;
    private int numFireflies;

    private static float lightRange = 0.025f;
    private float lightTrans = lightRange / 5;

    // Start is called before the first frame update
    void Start()
    {
        capacity = 10;
        numFireflies = 0;
       
        gameLightInner.transform.SetPositionAndRotation(new Vector3(0, 5, 0), new Quaternion(0, 0, 0, 0));
        gameLightOuter.transform.SetPositionAndRotation(new Vector3(0, 7, 0), new Quaternion(0, 0, 0, 0));
        gameLightDistant.transform.SetPositionAndRotation(new Vector3(0, 11, 0), new Quaternion(0, 0, 0, 0));

        gameLightInner.GetComponent<Light>().range = 7;
        gameLightOuter.GetComponent<Light>().range = 9;
        gameLightDistant.GetComponent<Light>().range = 33;

        gameLightInner.GetComponent<Light>().intensity = 1.5f;
        gameLightOuter.GetComponent<Light>().intensity = 0.85f;
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
    { /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCoroutine(SmoothLight(lightRange));
        if (Input.GetKeyDown(KeyCode.R) && lightComp.range > 7)
            StartCoroutine(SmoothLight(-lightRange)); */
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.gameObject.transform.position.x - this.transform.position.x < 1f && other.gameObject.transform.position.z - this.transform.position.z < 1f)
            {
                if (gameLightInner.GetComponent<Light>().range > 7)
                {
                    numFireflies--;
                    StartCoroutine(SmoothLight(-lightRange));
                }
                Destroy(other.gameObject);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Firefly")
        {
            StartCoroutine(Vacuum(other.gameObject));

            if (Input.GetMouseButton(0) && other.gameObject.transform.position.x - this.transform.position.x < 1f && other.gameObject.transform.position.z - this.transform.position.z < 1f)
            {
                {
                    if (numFireflies < capacity)
                    {
                        numFireflies++;
                        StartCoroutine(SmoothLight(lightRange));
                        Destroy(other.gameObject);
                    }
                }
            }
        }

    }

    IEnumerator SmoothLight(float range)
    {
        Light lInner = gameLightInner.GetComponent<Light>();
        float time = Time.time;
        while (Time.time - time < 0.5f)
        {
            gameLightInner.GetComponent<Light>().range += range;
            gameLightInner.transform.Translate(new Vector3(0, (lightTrans * range / Mathf.Abs(range)), 0));
            gameLightOuter.GetComponent<Light>().range += 1.5f * range;
            gameLightOuter.transform.Translate(new Vector3(0, (1.5f * lightTrans * range / Mathf.Abs(range)), 0));
            yield return null;
        }
    }

    IEnumerator Vacuum(GameObject obj)
    {
        float xComp = 0;
        float zComp = 0;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            xComp = 0;
            zComp = 0;
            if (obj.transform.position.x > this.transform.position.x)
                xComp = -0.1f;
            else
                xComp = 0.1f;
            if (obj.transform.position.z > this.transform.position.z)
                zComp = -0.1f;
            else
                zComp = 0.1f;
        }
        obj.gameObject.transform.Translate(xComp, 0, zComp);
        yield return null;
    }

}
