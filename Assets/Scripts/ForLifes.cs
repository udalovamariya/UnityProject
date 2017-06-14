using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLifes : MonoBehaviour
{

    public static ForLifes NLivesCounter;

    public Sprite fullHP;
    public Sprite noneHP;

    private UI2DSprite[] Lives;

    private void Awake()
    {
        NLivesCounter = this;
        Lives = new UI2DSprite[3];
        GetLives();
    }

    public void Clear(int i)
    {
        Lives[3 - i].sprite2D = noneHP;
    }

    private void GetLives()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Lives[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
        }
    }

}