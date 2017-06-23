using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CrystalColor
{
    Red = 0,
    Blue = 1,
    Green = 2
}

public class MasOfWin : MonoBehaviour
{

    #region Fields

    public int NumberOfFruit;
    public static bool IsSound = true;
    public AudioClip Music;
    private AudioSource MusicSource;

    public MyButton CloseButton;
    public MyButton BlackBackground;
    public MyButton NextButton;
    public MyButton ReplayButton;
    public UILabel Fruits;
    public UILabel Coins;
    public UI2DSprite Crystal1;
    public UI2DSprite Crystal2;
    public UI2DSprite Crystal3;
    public Sprite CrystalNotGet;
    public Sprite CrystalSprite1;
    public Sprite CrystalSprite2;
    public Sprite CrystalSprite3;

    #endregion

    void Start()
    {
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.clip = Music;
        MusicSource.loop = false;

        if (IsSound)
        {
            MusicSource.Play();
        }

        CloseButton.SignalOnClick.AddListener(Close);
        BlackBackground.SignalOnClick.AddListener(Close);
        NextButton.SignalOnClick.AddListener(Next);
        ReplayButton.SignalOnClick.AddListener(Replay);
    }

    #region Methods

    void Close()
    {
        SceneManager.LoadScene("mainscene");
        Destroy(this.gameObject);
        Time.timeScale = 1;
    }
    void Next()
    {
        SceneManager.LoadScene("mainscene");
        Time.timeScale = 1;

    }
    void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ForCrys.CrysCounter.GetCryss();
    }

    public void SetCoins(int allLevelCoins)
    {
        Coins.text = allLevelCoins.ToString();
    }
    public void SetFruits(int fruits, int level)
    {
        Fruits.text = fruits.ToString();
        if (NumberOfFruit == fruits)
        {
            LevelController.IsLevel1FruitCollected = true;
            if (level == 1)
                PlayerPrefs.SetInt("SOBRANU_VSE_Fr", 1);
            if (level == 2)
                PlayerPrefs.SetInt("SOBRANU_VSE_Fr2", 1);
            PlayerPrefs.Save();
        }
    }
    public void SetCrystal(Dictionary<CrystalColor, bool> cp, int level)
    {
		if (!cp [CrystalColor.Blue])
			Crystal1.sprite2D = CrystalNotGet;
		else
			Crystal1.sprite2D = CrystalSprite1;
		
		if (!cp [CrystalColor.Red])
			Crystal2.sprite2D = CrystalNotGet;
		else
			Crystal2.sprite2D = CrystalSprite2;
		
		if (!cp[CrystalColor.Green])
            Crystal3.sprite2D = CrystalNotGet;
		else
			Crystal3.sprite2D = CrystalSprite3;
		if (cp[CrystalColor.Blue] && cp[CrystalColor.Red] && cp[CrystalColor.Green])
        {
            LevelController.SOBRANU_VSE_KRISTALU = true;
            if (level == 1)
            {
                PlayerPrefs.SetInt("SOBRANU_VSE_KRISTALU", 1);
            }
            if (level == 2)
            {
                PlayerPrefs.SetInt("SOBRANU_VSE_KRISTALU2", 1);
            }
            PlayerPrefs.Save();
        }
    }

    #endregion

}