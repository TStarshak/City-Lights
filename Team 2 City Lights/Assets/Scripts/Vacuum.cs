using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private GameObject firefly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float xComp = 0;
        float zComp = 0;
        if(other.tag == "Firefly")
        {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (firefly.transform.position.x > this.transform.position.x)
                    xComp = -0.001f;
                else
                    xComp = 0.001f;
                if (firefly.transform.position.z > this.transform.position.z)
                    zComp = -0.001f;
                else
                    zComp = 0.001f;
            }
        }
        firefly.transform.Translate(xComp, 0, zComp);
    }
}
