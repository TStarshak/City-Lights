using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer render;
    private bool vacuum;
    private PlayerData playerData;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("isWalking", false);
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>("Player_Char");
        vacuum = false;
        playerData = PlayerState.localPlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerState.localPlayerData.inDangerState)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<SpriteRenderer>().material.SetColor(0,Color.red);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && !playerData.isDead)
        {
            if (!vacuum)
                anim.SetBool("isWalking", true);
            if (Input.GetKey(KeyCode.W))
                anim.SetBool("isForward", false);
            else
                anim.SetBool("isForward", true);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isSide", true);
                if (Input.GetKey(KeyCode.D))
                    render.flipX = false;
                else
                    render.flipX = true;
            }
            else
                anim.SetBool("isSide", false);
        }
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mouse = Input.mousePosition;
            if (mouse.x > Screen.width / 2)
            {
                render.flipX = false;
            }
            else
                render.flipX = true;
        }
    }


    private void OnMouseDown()
    {
        if (!playerData.isDead)
        {
            vacuum = true;
            anim.SetBool("isVac", true);
        }
    }

    private void OnMouseUp()
    {
        vacuum = false;
        anim.SetBool("isVac", false);
    }

}