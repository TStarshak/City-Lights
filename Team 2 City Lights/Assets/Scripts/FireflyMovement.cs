using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    // Variables for object circling
    // private float flightRotation = 0;
    // private float flightSpeed = 10f;

    // Variables for object floating
    //0 = regular, 3 = most rare
    public float activationDistance = 5.0f;
    public int rarity;
    private float floatAmplitude = 0.25f;
    private float floatFrequency = 0.7f;
    private bool canMove;
    public bool inVac;
    private float offset;
    public Quaternion init;
    private bool fleeing;
    public GameObject Player;

    // Store the position
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        rarity = 0;
        fleeing = false;
        int r = Random.Range(0, 101);
        if (r <= 3)
        {
            rarity = 3;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/firefly/rare3");
        }
        else if (r <= 8)
        {
            rarity = 2;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/firefly/rare3");
        }
        else if (r <= 15)
        {
            rarity = 1;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/firefly/rare");
        }
        else
        {
            rarity = 0;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/firefly/firefly_slide");
        }
        posOffset = new Vector3(transform.position.x, 1, transform.position.z);
        canMove = true;
        inVac = false;
        offset = Random.value + 0.1f;
        init = transform.rotation;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        tempPos = this.transform.position;
        // Float up and down
        if (!fleeing)
        {
            tempPos.y = posOffset.y;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
            transform.position = tempPos;
        }
        

        Vector3 dir = this.transform.position - Player.transform.position;

        if (!inVac && dir.magnitude < activationDistance )
            StartCoroutine(Flee(dir));

        if (canMove && !inVac)
            StartCoroutine(Move());

        this.transform.rotation = init;
    }
    
    IEnumerator Flee(Vector3 dir)
    {
        fleeing = true;
        float theta = Random.Range(0, 46) * Mathf.PI / 180;
        theta *= (Random.Range(0, 1) < 0.5) ? 1 : -1;
        Vector3 move = new Vector3(dir.x * Mathf.Cos(theta) + dir.z * Mathf.Sin(theta), 0, dir.z * Mathf.Cos(theta) - dir.x * Mathf.Sin(theta)).normalized;

        float time = Time.time;
        while (Time.time - time < 1.5f)
        {
            if (!inVac)
                transform.Translate(move * Time.deltaTime * 1f);
            yield return new WaitForSeconds(0.05f);
        }
        fleeing = false;
        yield return null;
    }

    IEnumerator Move()
    {
        canMove = false;
        float time = Time.time;
        while (Time.time - time < 0.5f)
        {
            if (!inVac && !fleeing)
            {
                int r = (int)(Random.Range(0, 3) + 0.5f);
                if (r == 0)
                    transform.Translate(transform.forward * Time.deltaTime * 1.5f);
                if (r == 1)
                    transform.Translate(transform.right * Time.deltaTime * 1.5f);
                if (r == 2)
                    transform.Translate(-transform.forward * Time.deltaTime * 1.5f);
                if (r == 3)
                    transform.Translate(-transform.right * Time.deltaTime * 1.5f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        canMove = true;
        yield return null;
    }
}
