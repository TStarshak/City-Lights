using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private GameObject gameLight;
    private Light lightComp;
    private Collider colliderComp;
    [SerializeField] private GameObject firefly;
    [SerializeField] private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        lightComp = gameLight.GetComponent<Light>();
        colliderComp = GetComponent<Collider>();
        // Set color and position
        lightComp.color = Color.yellow;

        // Set the position (or any transform property)
        lightComp.range = 10f;
        lightComp.intensity = 5f;
        lightComp.shadows = LightShadows.Soft;
        for(int i = 0; i < 5; i++)
        {
            Instantiate(firefly, new Vector3((float)(i/2), 0.5f, (float) Mathf.Sqrt((i-2)*(i-1))), new Quaternion(0f, 0f, 0f, 0f));
        }
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemy, new Vector3((float)Mathf.Sqrt((i - 2) * (i - 1)), 0.5f, (float)(-i/2)), new Quaternion(0f, 0f, 0f, 0f));
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            lightComp.range = lightComp.range + 0.5f;
            lightComp.transform.Translate(new Vector3(0, 0.1f, 0));
        }
        if (Input.GetKeyDown(KeyCode.R) && lightComp.range > 7)
        {
            lightComp.range = lightComp.range - 0.5f;
            lightComp.transform.Translate(new Vector3(0, -0.1f, 0));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Firefly")
        {
            lightComp.range = lightComp.range + 2f;
            lightComp.transform.Translate(new Vector3(0, 0.4f, 0));
            Destroy(other.gameObject);
        }
        if (other.tag == "Enemy")
        {
            lightComp.range = lightComp.range - 2f;
            lightComp.transform.Translate(new Vector3(0, -0.4f, 0));
            Destroy(other.gameObject);
        }
    }



}
