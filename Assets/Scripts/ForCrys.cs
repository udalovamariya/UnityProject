using UnityEngine;

public class ForCrys : MonoBehaviour
{

    #region Fields

    public static ForCrys CrysCounter;

    public Sprite Crystal1;
    public Sprite Crystal2;
    public Sprite Crystal3;	

    public UI2DSprite[] Cryss;

    #endregion

    private void Awake()
    {
        CrysCounter = this;
        Cryss = new UI2DSprite[3];
        GetCryss();
    }

    #region Methods

    public void GetCrysThroughToken(string gemID)
    {

        if (gemID.Equals("gem-1"))
        {
            Cryss[0].sprite2D = Crystal1;
        }
        else if (gemID.Equals("gem-2"))
        {
            Cryss[1].sprite2D = Crystal2;
        }
        else if (gemID.Equals("gem-3"))
        {
            Cryss[2].sprite2D = Crystal3;
        }
    }
    public void GetCryss()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Cryss[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
        }
    }

    #endregion

}