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
        if (other.gameObject.tag.Equals("Firefly"))
        {
            if (Lighting.numFireflies < Lighting.capacity && Vacuum.isOn)
            {
                SendMessageUpwards("onFireflyEnter");
            }
            if(Vacuum.isOn)
                Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Enemy"))
        {
            GameObject obj = other.gameObject;
            Destroy(other);
            obj.GetComponent<Animator>().SetBool("hasAttacked", true);
            StartCoroutine(WaitAndDestroy(obj));
            if (Lighting.numFireflies > 0)
            {
                SendMessageUpwards("onEnemyEnter");
            }

        }
    }


    IEnumerator WaitAndDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(1.833f/2f);
        float time = Time.time;

        while (Time.time - time < 0.3f)
        {
            obj.GetComponent<SpriteRenderer>().color = new Color(obj.GetComponent<SpriteRenderer>().color.r, obj.GetComponent<SpriteRenderer>().color.g, obj.GetComponent<SpriteRenderer>().color.b,obj.GetComponent<SpriteRenderer>().color.a / 2);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(obj);
        yield return null;
    }

}
