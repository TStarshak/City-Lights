using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Vacuum vac;
    private UnityEngine.AI.NavMeshAgent Shadoow; //Calls the mesh agent that tells our enemy what paths are travesable
    private GameObject Player;
    public float activationDistance = 5.0f;
    private static bool isOn;
    private float elapsedTime = 0.0f;
    public float secondsBetweenMove;
    private bool move = true;
    private Quaternion rot;
    private SpriteRenderer rend;
    private bool lookRight;
    private Animator anim;
    private Color shadeAlpha;

    // Start is called before the first frame update
    void Start()
    {
        vac = GameObject.Find("Vacuum").GetComponent<Vacuum>();
        rot = transform.rotation;
        Shadoow = GetComponent<UnityEngine.AI.NavMeshAgent>(); //applies the agent to our lovely enemy
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lookRight = true;
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerState.localPlayerData.isDead)
            StartCoroutine(shadeDeath());

        this.transform.rotation = rot;
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if ((distance < activationDistance) && Vacuum.isOn)
        {
            move = false;
            anim.SetBool("Hide", true);
          
        }
        else if (move)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

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


            Shadoow.SetDestination(newPos);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= secondsBetweenMove) move = true;
            anim.SetBool("Hide", true);
        }
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(secondsBetweenMove);

    }

    private IEnumerator shadeDeath()
    {
        yield return new WaitForSeconds(10f);
        // If it's shadow hour, the Shades are immortal
        if (!ShadowTimerController.shadowHour)
        {
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
