using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinWindow : MonoBehaviour {

    public int numberOfFruit;
    public static bool isSound = true;
    public AudioClip music;
    AudioSource musicSource;

    public MyButton closeButton;
    public MyButton blackBackground;
    public MyButton nextButton;
    public MyButton replayButton;
    public UILabel fruits;
    public UILabel coins;
    public UI2DSprite crystal1;
    public UI2DSprite crystal2;
    public UI2DSprite crystal3;
    public Sprite crystalNotGet;
    public Sprite crystalSprite1;
    public Sprite crystalSprite2;
    public Sprite crystalSprite3;

    // Use this for initialization
    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = false;
        if (isSound)
            musicSource.Play();
        closeButton.signalOnClick.AddListener(this.close);
        blackBackground.signalOnClick.AddListener(this.close);
        nextButton.signalOnClick.AddListener(this.next);
        replayButton.signalOnClick.AddListener(this.replay);
    }

    // Update is called once per frame
    void close()
    {
        SceneManager.LoadScene("chooselevel");
        Destroy(this.gameObject);
        Time.timeScale = 1;
    }

    void next()
    {
        SceneManager.LoadScene("chooselevel");
        Time.timeScale = 1;

    }
    void replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void setCoins(int coins)
    {
        this.coins.text = coins.ToString();
    }

    public void setFruits(int fruits, int level)
    {
        this.fruits.text = fruits.ToString();
        if (this.numberOfFruit == fruits)
        {
            LevelController.IsLevel1FruitCollected = true;
            if (level == 1)
                PlayerPrefs.SetInt("isLevel1FruitCollected", 1);
            if (level == 2)
                PlayerPrefs.SetInt("isLevel2FruitCollected", 1);
            PlayerPrefs.Save();
        }
    }
    public enum CrystalColor
    {
        Blue = 0,
        Red = 1,
        Green = 2
    }

    public void setCrystal(Dictionary<CrystalColor, bool> cp, int level)
    {

        if (!(cp.ContainsKey(CrystalColor.Blue)))
            crystal1.sprite2D = crystalNotGet;
        if (!(cp.ContainsKey(CrystalColor.Green)))
            crystal2.sprite2D = crystalNotGet;
        if (!(cp.ContainsKey(CrystalColor.Red)))
            crystal3.sprite2D = crystalNotGet;
        if ((cp.ContainsKey(CrystalColor.Blue) && cp.ContainsKey(CrystalColor.Red) && cp.ContainsKey(CrystalColor.Green)))
        {
            LevelController.IsLevel1CrysralsCollected = true;
            if (level == 1)
            {
                PlayerPrefs.SetInt("isLevel1CrysralsCollected", 1);
            }
            if (level == 2)
            {
                PlayerPrefs.SetInt("isLevel2CrysralsCollected", 1);
            }
            PlayerPrefs.Save();
        }
    }


}