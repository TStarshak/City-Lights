using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer render;
    private bool vacuum;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("isWalking", false);
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>("Player_Char");
        vacuum = false;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if(!vacuum)
                    anim.SetBool("isWalking", true);
                if (Input.GetKey(KeyCode.W))
                    anim.SetBool("isForward", false);
                if (Input.GetKey(KeyCode.S))
                    anim.SetBool("isForward", true);
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                    anim.SetBool("isSide", true);
                else
                    anim.SetBool("isSide", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isForward", true);
            }
            Vector3 mouse = Input.mousePosition;
            if (mouse.x > Screen.width / 2)
            {
                render.flipX = false;
            }
            else
                render.flipX = true;
    }


    private void OnMouseDown()
    {
        vacuum = true;
        anim.SetBool("isVac", true);
    }

    private void OnMouseUp()
    {
        vacuum = false;
        anim.SetBool("isVac", false);
    }

}