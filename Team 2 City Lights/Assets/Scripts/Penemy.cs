using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Penemy : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;
    public float secondsBetweenSpawn;
    private float elapsedTime = 0.0f;
    public GameObject Player;
    public float activationDistance = 5.0f;
    private NavMeshAgent pMesh;
    private Quaternion rot;
    private Animator anim;
    private SpriteRenderer rend;
    private bool lookRight = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rot = this.transform.rotation;
        pMesh = GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
        rend = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Update()
    {
        this.transform.rotation = rot;
        //timer for spawn
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn) 
        {
            elapsedTime = 0;
            Instantiate(Projectile, transform.position, transform.rotation );
            anim.SetTrigger("Attack");
        }

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance > activationDistance)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;
            
            pMesh.SetDestination(newPos);
            float xPos = dirToPlayer.x;
            if (xPos > 0 && lookRight)
            {
                lookRight = false;
                rend.flipX = false;
            }
            else if (xPos < 0 && !lookRight)
            {
                lookRight = true;
                rend.flipX = true;
            }

        }
        else
        {
            pMesh.SetDestination(transform.position);
        }
    }

}


