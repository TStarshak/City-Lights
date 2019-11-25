using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerBackgroundController : MonoBehaviour
{
    private SpriteRenderer spr;
    private Sprite[] colorSprites;
    int x = 0;
    int spacer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //spr = GetComponent<SpriteRenderer>();
        //Sprite img = this.gameObject.GetComponent<Image>();
        colorSprites = Resources.LoadAll<Sprite>("UI/colour");
        this.gameObject.GetComponent<Image>().sprite = colorSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        //int x = 0;
        //spr.sprite = sprites[x];
        //x++;
        this.gameObject.GetComponent<Image>().sprite = colorSprites[x];
        spacer++;
        if (spacer==2) {
            spacer = 0;
            x++;
            if (x == 50)
            {
                x = 49;
            }
        }
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
