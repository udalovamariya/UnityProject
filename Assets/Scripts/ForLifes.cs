using UnityEngine;

public class ForLifes : MonoBehaviour
{

    #region Fields

    public static ForLifes NLivesCounter;

    public Sprite FullHealth;
    public Sprite NoneHealth;

    public UI2DSprite[] Lives;

    #endregion

    private void Awake()
    {
        NLivesCounter = this;
        Lives = new UI2DSprite[3];
        GetLives();
    }

    #region Methods

    public void Clear(int i)
    {
        Lives[3 - i].sprite2D = NoneHealth;
    }
    public void Add(int i)
    {
        Lives[i - 1].sprite2D = FullHealth;
    }

    private void GetLives()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Lives[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
        }
    }

    #endregion

}