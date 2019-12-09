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
    private bool idle;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("isWalking", false);
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>("Player_Char");
        vacuum = false;
        playerData = PlayerState.localPlayerData;
        idle = false;
        changeAnimator();
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
            idle = false;
            anim.SetBool("isWalking", true);
            if (Input.GetKey(KeyCode.W))
                anim.SetBool("isForward", false);
            else
                anim.SetBool("isForward", true);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isSide", true);
                if (Input.GetKey(KeyCode.A))
                    render.flipX = true;
                if (Input.GetKey(KeyCode.D))
                    render.flipX = false;
            }
            else
                anim.SetBool("isSide", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            if (!vacuum && !idle)
            {
                idle = true;
                anim.SetTrigger("isIdle");
            }
        }
        if (((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || Input.GetKey(KeyCode.Mouse0)) && !playerData.isDead)
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

    public void changeAnimator()
    {
        //Get Skill level
        int i =PlayerState.currentUpgrades.highestSkillLevel();

        switch (i)
        {
            case 1:
                anim.runtimeAnimatorController = Resources.Load("Animation/CharAnim1") as RuntimeAnimatorController;
                break;

            case 2:
                anim.runtimeAnimatorController = Resources.Load("Animation/CharAnim2") as RuntimeAnimatorController;
                break;

            case 3:
                anim.runtimeAnimatorController = Resources.Load("Animation/CharAnim3") as RuntimeAnimatorController;
                break;

            case 4:
                anim.runtimeAnimatorController = Resources.Load("Animation/CharAnim4") as RuntimeAnimatorController;
                break;

            default:
                anim.runtimeAnimatorController = Resources.Load("Animation/CharAnim0") as RuntimeAnimatorController;
                break;
        }
    }


    private void OnMouseDown()
    {
        if (!playerData.isDead)
        {
            idle = false;
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