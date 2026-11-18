using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour
{
    SpriteRenderer spr;
    public enumWorkResult menuType;

    public Sprite sprite { get { return spr.sprite; } set { spr.sprite = value; } }

    public Sprite getSprite()
    {
        return spr.sprite;
    }

    public void setSprite(Sprite _Spr)
    {
        spr.sprite = _Spr;
    }

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }
}
