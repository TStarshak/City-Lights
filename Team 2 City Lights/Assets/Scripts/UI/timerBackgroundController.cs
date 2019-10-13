using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerBackgroundController : MonoBehaviour
{
    private SpriteRenderer spr;
    private Sprite [] sprites;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("bg3");
    }

    // Update is called once per frame
    void Update()
    {
        int x = 0;
        spr.sprite = sprites[x];
        x++;
    }
}
