using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBackgroundController : MonoBehaviour
{
    private SpriteRenderer spr;
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        //spr = GetComponent<SpriteRenderer>();
        //Sprite img = this.gameObject.GetComponent<Image>();
        sprites = Resources.LoadAll<Sprite>("bg3");
        this.gameObject.GetComponent<Image>().sprite = sprites[1];
    }

    // Update is called once per frame
    void Update()
    {
        //int x = 0;
        //spr.sprite = sprites[x];
        //x++;
    }
/*
    IEnumerator timer()
    {
        int x = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            
        }

    }*/
}
