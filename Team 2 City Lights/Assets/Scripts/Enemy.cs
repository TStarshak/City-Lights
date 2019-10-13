using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Shadoow; //Calls the mesh agent that tells our enemy what paths are travesable
    public GameObject Player;
    public float activationDistance = 17.0f; //This distance determines how close you have to get to make the enemy chase after you
    private Color shadeAlpha;

    void Start()
    {
        Shadoow=GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
        if (!this.name.Equals("Shadoow"))
        {
            shadeAlpha = this.GetComponent<SpriteRenderer>().material.color;
            this.GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, 0);
            Shadoow.enabled = false;
            StartCoroutine(shadeDeath());
            StartCoroutine(shadeSpawn());
        }
    }

    void Update() //this all just tells the enemy to chase the player
    {
        float distance=Vector3.Distance(transform.position, Player.transform.position);

        if (Shadoow.enabled && distance<activationDistance)
        {
            Vector3 dirToPlayer=transform.position-Player.transform.position;
            Vector3 newPos=transform.position-dirToPlayer;
            Shadoow.SetDestination(newPos);
        }
    }

    private IEnumerator shadeDeath()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    private IEnumerator shadeSpawn()
    {
        for (int i = 0; i < 40; i++)
        {
            shadeAlpha = this.GetComponent<SpriteRenderer>().material.color;
            this.GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, shadeAlpha.a + 0.025f);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        Shadoow.enabled = true;
    }
}
