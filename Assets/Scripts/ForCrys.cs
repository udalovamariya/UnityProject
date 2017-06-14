using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCrys : MonoBehaviour
{
    public static ForCrys GemsCounter;

    public Sprite gem1;
    public Sprite gem2;
    public Sprite gem3;

    private UI2DSprite[] Gems;

    private void Awake()
    {
        GemsCounter = this;
        Gems = new UI2DSprite[3];
        GetGems();
    }

    public void GetGemThroughToken(string gemID)
    {
        if (gemID.Equals("gem-1"))
        {
            Gems[0].sprite2D = gem1;
        }
        else if (gemID.Equals("gem-2"))
        {
            Gems[1].sprite2D = gem2;
        }
        else if (gemID.Equals("gem-3"))
        {
            Gems[2].sprite2D = gem3;
        }
    }

    private void GetGems()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Gems[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
        }
    }

}