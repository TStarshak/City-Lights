using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>("Player_Char");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);
        Vector3 mouse = Input.mousePosition;
        if (mouse.x > Screen.width / 2)
        {
            render.flipX = false;
        }
        else
            render.flipX = true;

    }
}
