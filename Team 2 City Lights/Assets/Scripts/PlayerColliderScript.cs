using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag.Equals("Firefly"))
        {
            if (Lighting.numFireflies < Lighting.capacity && Vacuum.isOn)
            {
                SendMessageUpwards("onFireflyEnter");
                Destroy(other.gameObject);
                Lighting.numFireflies += 1;
            }
        }
        else if (other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(other.gameObject);
            if (Lighting.numFireflies > 0)
            {
                SendMessageUpwards("onEnemyEnter");
                Lighting.numFireflies -= 1;
            }
        }
    }

}
