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


    // Start is called before the first frame update
    void Start()
    {
        vac = GameObject.Find("Vacuum").GetComponent<Vacuum>();
        Shadoow = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if ((distance < activationDistance) && Vacuum.isOn)
        {
            move = false;
          
        }
        else if (move)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            Shadoow.SetDestination(newPos);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= secondsBetweenMove) move = true;
        }
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(secondsBetweenMove);

    }

}
