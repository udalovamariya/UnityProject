using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinWindow : MonoBehaviour
{
    public enum CrystalColor
    {
        Blue = 0,
        Red = 1,
        Green = 2
    }

    #region Fields

    public int NumberOfFruit;
    public static bool IsSound = true;
    public AudioClip Music;
    AudioSource MusicSource;
  
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
            MusicSource.Play();
        CloseButton.SignalOnClick.AddListener(this.Close);
        BlackBackground.SignalOnClick.AddListener(this.Close);
        NextButton.SignalOnClick.AddListener(this.Next);
        ReplayButton.SignalOnClick.AddListener(this.Replay);
    }

    #region Methods

    void Close()
    {
        SceneManager.LoadScene("chooselevel");
        Destroy(this.gameObject);
        Time.timeScale = 1;
    }
    void Next()
    {
        SceneManager.LoadScene("chooselevel");
        Time.timeScale = 1;

    }
    void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void SetCoins(int coins)
    {
        Coins.text = coins.ToString();
      
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
                PlayerPrefs.SetInt("SOBRANU_VSE_Fr", 1);
            PlayerPrefs.Save();
        }
    }/*
    public void SetCrystal(Dictionary<CrystalColor, bool> cp, int level)
    {

        if (!(cp.ContainsKey(CrystalColor.Blue)))
            Crystal1.sprite2D = CrystalNotGet;
        if (!(cp.ContainsKey(CrystalColor.Red)))
            Crystal2.sprite2D = CrystalNotGet;
        if (!(cp.ContainsKey(CrystalColor.Green)))
            Crystal3.sprite2D = CrystalNotGet;
        if ((cp.ContainsKey(CrystalColor.Blue) && cp.ContainsKey(CrystalColor.Red) && cp.ContainsKey(CrystalColor.Green)))
        {

           SOBRANU_VSE_KRISTALU = true;
            if (level == 1)
            {
                PlayerPrefs.SetInt("SOBRANU_VSE_KRISTALU", 1);
            }
            if (level == 2)
            {
                PlayerPrefs.SetInt("SOBRANU_VSE_KRISTALU", 1);
            }
            PlayerPrefs.Save();
        }
    }*/

    #endregion

}