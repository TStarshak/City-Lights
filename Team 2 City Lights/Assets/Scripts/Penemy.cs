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
    private Color shadeAlpha;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rot = this.transform.rotation;
        pMesh = GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
        rend = GetComponent<SpriteRenderer>();
        shadeAlpha = GetComponent<SpriteRenderer>().material.color;
        GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, 0);
        pMesh.enabled = false;
        StartCoroutine(shadeSpawn());
        StartCoroutine(shadeDeath());
    }
    // Start is called before the first frame update
    void Update()
    {
        this.transform.rotation = rot;
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn && pMesh.enabled)
            if (!PlayerState.localPlayerData.isDead)
            {
                this.transform.rotation = rot;
                //timer for spawn
                elapsedTime += Time.deltaTime;
                if (elapsedTime > secondsBetweenSpawn && pMesh.enabled)
                {
                    elapsedTime = 0;
                    Instantiate(Projectile, transform.position, transform.rotation);
                    anim.SetTrigger("Attack");
                }

                float distance = Vector3.Distance(transform.position, Player.transform.position);
                if (distance > activationDistance && pMesh.enabled)
                {
                    Vector3 dirToPlayer = transform.position - Player.transform.position;

                    Vector3 newPos = transform.position - dirToPlayer;

                    if (distance > activationDistance && pMesh.enabled)
                    {
                        Vector3 dirToPlayer = transform.position - Player.transform.position;

                        Vector3 newPos = transform.position - dirToPlayer;

                        pMesh.SetDestination(newPos);
                        float xPos = dirToPlayer.x;
                        if (xPos > 0 && lookRight)
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
                    else if (pMesh.enabled)
                    {
                        pMesh.SetDestination(transform.position);
                    }
                }
            }

        IEnumerator shadeSpawn()
        {
            for (int i = 0; i < 40; i++)
            {
                shadeAlpha = GetComponent<SpriteRenderer>().material.color;
                GetComponent<SpriteRenderer>().material.color = new Color(shadeAlpha.r, shadeAlpha.g, shadeAlpha.b, shadeAlpha.a + 0.025f);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                yield return new WaitForSeconds(0.001f);
            }
            pMesh.enabled = true;
        }

        IEnumerator shadeDeath()
        {
            yield return new WaitForSeconds(15f);
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
}


