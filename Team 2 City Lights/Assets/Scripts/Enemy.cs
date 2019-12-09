using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Shadoow; //Calls the mesh agent that tells our enemy what paths are travesable
    public GameObject Player;
    public float activationDistance = 25.0f; //This distance determines how close you have to get to make the enemy chase after you
    private Quaternion rot;
    private SpriteRenderer rend;
    private bool lookRight;
    private Animator anim;
    private Color shadeAlpha;
    void Start()
    {
        rot = transform.rotation;
        Shadoow = GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lookRight = true;

        if (!this.name.Equals("Shadoow"))
        {
            shadeAlpha = GetComponent<SpriteRenderer>().material.color;
            GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, 0);
            Shadoow.enabled = false;
            StartCoroutine(shadeSpawn());
            StartCoroutine(shadeDeath());
        }

    }

    void Update() //this all just tells the enemy to chase the player
    {
        //If the player is dead, destroy the shades to get them off the screen
        if (PlayerState.localPlayerData.isDead)
            StartCoroutine(shadeDeath());

        float distance=Vector3.Distance(transform.position, Player.transform.position);
        Shadoow.transform.rotation = rot;
        if (PauseController.isPaused == false)
        {
            if (distance < activationDistance)
            {
                Vector3 dirToPlayer = transform.position - Player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;
                if (Shadoow.enabled)
                {
                    Shadoow.SetDestination(newPos);
                }
                float xPos = dirToPlayer.x;
                if (xPos > 0 && lookRight)
                {
                    lookRight = false;
                    rend.flipX = true;
                }
                else if (xPos < 0 && !lookRight)
                {
                    lookRight = true;
                    rend.flipX = false;
                }
            }
        }
        else
        {
            Shadoow.SetDestination(this.gameObject.transform.position);
        }
    }
    private IEnumerator shadeSpawn()
    {
        for (int i = 0; i < 40; i++)
        {
            shadeAlpha = GetComponent<SpriteRenderer>().material.color;
            GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, shadeAlpha.a + 0.025f);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        Shadoow.enabled = true;
    }

    private IEnumerator shadeDeath()
    {
        yield return new WaitForSeconds(10f);
        // If it's shadow hour, the Shades are immortal
        if (!ShadowTimerController.shadowHour){
            for (int i = 0; i < 20; i++)
            {
                shadeAlpha = GetComponent<SpriteRenderer>().material.color;
                GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, shadeAlpha.a - 0.05f);
                yield return new WaitForSeconds(0.001f);
            }
            Destroy(this.gameObject);
        }
    }
}
