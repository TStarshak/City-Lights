using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColliderScript : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Web")
        {
            PlayerState.localPlayerData.movementSpeed = 8;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Firefly"))
        {
            int fireflyValue = other.gameObject.GetComponent<FireflyMovement>().rarity;
            // Make sure the value of collecting the firefly won't exceed the vaculamp capacity
            if ((Lighting.numFireflies + fireflyValue <= Lighting.capacity) && Vacuum.isOn)
            {
                Destroy(other.gameObject);
                SendMessageUpwards("onFireflyEnter", fireflyValue);
            }
            else if(Vacuum.isOn)
                Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Enemy"))
        {
            GameObject obj = other.gameObject;
            Destroy(other);
            obj.GetComponent<Animator>().SetBool("hasAttacked", true);
            StartCoroutine(WaitAndDestroy(obj));
            SendMessageUpwards("onEnemyEnter");

        }
        else if (other.gameObject.tag.Equals("Web"))
        {
            PlayerState.localPlayerData.movementSpeed = 5;
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
