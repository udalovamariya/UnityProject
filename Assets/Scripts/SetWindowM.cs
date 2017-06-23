using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetWindowM : MonoBehaviour
{

    #region Fields

    private bool MusicPlay;
    private bool SoundPlay;
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

    #endregion

    public void Display()
    {
        ChildForSettings = NGUITools.AddChild(UICamera.first.transform.parent.gameObject, PanelOfSettings);
        InitGameObjects();
        SetOfMusicAndSound();
        LocateSettingsFrame();
        AddListeners();
    }

    private void InitGameObjects()
    {
        BackGrForButton = GameObject.Find("2DSprite");
        CloseButton = GameObject.Find("closebutton");
        MusicButton = GameObject.Find("MusicButton");
        SoundButton = GameObject.Find("SoundButton");
    }
    void PlaySound()
    {
        if (!SoundPlay)
        {
            SoundPlay = true;
            PlayerPrefs.SetInt("sound", 1);
            SoundButton.GetComponent<UI2DSprite>().sprite2D = SoundON;
            SoundButton.GetComponent<UIButton>().normalSprite2D = SoundON;
           // MusicHead.Instance.SetSound(true);
        }
        else
        {
            SoundPlay = false;
            PlayerPrefs.SetInt("sound", 0);
            SoundButton.GetComponent<UI2DSprite>().sprite2D = SoundOFF;
            SoundButton.GetComponent<UIButton>().normalSprite2D = SoundOFF;
           // MusicHead.Instance.SetSound(false);
        }
    }
    void PlayMusic()
    {
        if (!MusicPlay)
        {
            MusicPlay = true;
            MusicHead.Instance.SetMusic(true);
            PlayerPrefs.SetInt("music", 1);
            MusicButton.GetComponent<UI2DSprite>().sprite2D = MusicON;
            MusicButton.GetComponent<UIButton>().normalSprite2D = MusicON;
            return;
        }
        else
        {
            MusicPlay = false;
            MusicHead.Instance.SetMusic(false);
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
            /* это твой метод, но он мне не нужен, но хотелось бысделать настройки как на главной сцене 
            ChildForSettings.transform.localPosition += new Vector3(120.0f, 750.0f, .0f);
             CloseButton.GetComponent<BoxCollider>().center = new Vector3(-2550.0f, -80.0f);
            CloseButton.GetComponent<BoxCollider>().size = new Vector3(8000.0f, 9000.0f);
            Time.timeScale = .0f;*/
        }
    }

    void SetOfMusicAndSound()
    {
        MusicPlay = Convert.ToBoolean(PlayerPrefs.GetInt("music", 1));
        SoundPlay = Convert.ToBoolean(PlayerPrefs.GetInt("sound", 1));

        if (!MusicPlay)
        {
            MusicButton.GetComponent<UI2DSprite>().sprite2D = MusicOFF;
        }

        if (!SoundPlay)
        {
            SoundButton.GetComponent<UI2DSprite>().sprite2D = MusicOFF;
        }

        MusicButton.GetComponent<MyButton>().SignalOnClick.AddListener(PlayMusic);
        SoundButton.GetComponent<MyButton>().SignalOnClick.AddListener(PlaySound);
    }

    void Destroy()
    {
        Destroy(ChildForSettings);
        Time.timeScale = 1.0f;
    }

    private void AddListeners()
    {
       BackGrForButton.GetComponent<MyButton>().SignalOnClick.AddListener(Destroy);
       CloseButton.GetComponent<MyButton>().SignalOnClick.AddListener(Destroy);
    }
}