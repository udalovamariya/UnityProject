using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setWindowM : MonoBehaviour
{
   
    private bool musicPlay;
    private bool soundPlay;
    public Sprite MusicOFF;
    public Sprite SoundOFF;
    public Sprite MusicON;
    public Sprite SoundON;
    public GameObject PanelOfSettings;
    private GameObject ChildForSettings;
    private GameObject BackGrForButton;
    private GameObject CloseButton;
    private GameObject MusicButton;
    private GameObject SoundButton;


    public void Display()
    {
        ChildForSettings = NGUITools.AddChild(UICamera.first.transform.parent.gameObject, PanelOfSettings);
        iniz_game_objects();
        set_of_MusicANDSound();
        LocateSettingsFrame();
        AddListeners();
    }

    private void iniz_game_objects()
    {
        BackGrForButton = GameObject.Find("2DSprite");
        CloseButton = GameObject.Find("closebutton");
        MusicButton = GameObject.Find("MusicButton");
        SoundButton = GameObject.Find("SoundButton");
    }
    void PlaySound()
    {
        if (!soundPlay)
        {
            soundPlay = true;
            PlayerPrefs.SetInt("sound", 1);
            SoundButton.GetComponent<UI2DSprite>().sprite2D = SoundON;
            SoundButton.GetComponent<UIButton>().normalSprite2D = SoundON;
            MusicHead.Instance.SetSoundMode(true);
        }
        else
        {
            soundPlay = false;
            PlayerPrefs.SetInt("sound", 0);
            SoundButton.GetComponent<UI2DSprite>().sprite2D = SoundOFF;
            SoundButton.GetComponent<UIButton>().normalSprite2D = SoundOFF;
            MusicHead.Instance.SetSoundMode(false);
        }
    }

    void PlayMusic()
    {
        if (!musicPlay)
        {
            musicPlay = true;
            MusicHead.Instance.SetMusicMode(true);
            PlayerPrefs.SetInt("music", 1);
            MusicButton.GetComponent<UI2DSprite>().sprite2D = MusicON;
            MusicButton.GetComponent<UIButton>().normalSprite2D = MusicON;
            return;
        }
        else
        {
            musicPlay = false;
            MusicHead.Instance.SetMusicMode(false);
            PlayerPrefs.SetInt("music", 0);
            MusicButton.GetComponent<UI2DSprite>().sprite2D = MusicOFF;
            MusicButton.GetComponent<UIButton>().normalSprite2D = MusicOFF;
        }
    }
    void LocateSettingsFrame()
    {
        string temp = SceneManager.GetActiveScene().name;
        if (temp.Equals("Level1") || temp.Equals("Level2"))
        {
            ChildForSettings.transform.localPosition += new Vector3(120.0f, 750.0f, .0f);
            CloseButton.GetComponent<BoxCollider>().center = new Vector3(-2550.0f, -80.0f);
            CloseButton.GetComponent<BoxCollider>().size = new Vector3(5800.0f, 3600.0f);
            Time.timeScale = .0f;
        }
    }

    void set_of_MusicANDSound()
    {
        musicPlay = Convert.ToBoolean(PlayerPrefs.GetInt("music", 1));
        soundPlay = Convert.ToBoolean(PlayerPrefs.GetInt("sound", 1));

        if (!musicPlay)
        {
            MusicButton.GetComponent<UI2DSprite>().sprite2D = MusicOFF;
        }

        if (!soundPlay)
        {
            SoundButton.GetComponent<UI2DSprite>().sprite2D = MusicOFF;
        }

        MusicButton.GetComponent<MyButton>().signalOnClick.AddListener(PlayMusic);
        SoundButton.GetComponent<MyButton>().signalOnClick.AddListener(PlaySound);
    }

    void Destroy()
    {
        Destroy(ChildForSettings);
        Time.timeScale = 1.0f;
    }

    private void AddListeners()
    {
       BackGrForButton.GetComponent<MyButton>().signalOnClick.AddListener(Destroy);
       CloseButton.GetComponent<MyButton>().signalOnClick.AddListener(Destroy);
    }
}