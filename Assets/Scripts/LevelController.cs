using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region Fields

    public static GameObject Objects;
    public int Lifes = 3;
    private int TotalDeath;
    Vector3 StartingPosition;
    
    public UILabel CoinsLabel;
    public UILabel FruitsLabel;
    public UI2DSprite CrystalSprites;

    public int AllLevelCoins = 0;
    public static int CollectedCoins;
    public MyButton PlayButton;
    public int NumberFr = 0;
    public GameObject WindowWin;
    public CrystalPanel CrystalPanel;
    public GameObject WinPanelPrefab;
    public static bool IsLevel1FruitCollected;
    public static bool IsLevel1CrysralsCollected;
    public static bool IsLevel2FruitCollected;
    public static bool IsLevel2CrysralsCollected;
    public static int AllFruitsOnLevel1 = 11;
    public AudioSource MusicSource;
    public AudioClip MusicClip;

    public static bool IsLevel1Completed; 
    public static LevelController Current;

    #endregion

    #region Methods

    private void Play()
    {
        SceneManager.LoadScene("chooselevel");
    }
    public void AddCoins(int i)
    {
        AllLevelCoins += i;
        SavingCoinsOfLevel.SavedMoneyAllLevel.sumCoins(AllLevelCoins);
    }
    public void AddFruits()
    {
        NumberFr += 1;
        FruitsLabel.text = NumberFr + "/" + AllFruitsOnLevel1;
    }

    public void SetStartPosition(Vector3 pos)
    {
        StartingPosition = pos;
    }
    void Awake()
    {
        Current = this;
        if ((SceneManager.GetActiveScene().name.Equals("mainscene"))){ }
        else  Current.SetStartPosition(HeroRabit.current.transform.position); 
        Objects = GameObject.Find("Objects");
        if (SceneManager.GetActiveScene().name.Equals("mainscene"))
        {
            PlayButton = GameObject.Find("PlayButton").GetComponent<MyButton>();
            PlayButton.signalOnClick.AddListener(Play);

            GameObject.Find("PlayButton").GetComponent<MyButton>()
               .signalOnClick.AddListener(Play);
            GameObject.Find("SetButton").GetComponent<MyButton>()
                .signalOnClick.AddListener(SetWindow);
        }
        SettingsForLevel();
        int Level1Crystals = PlayerPrefs.GetInt("isLevel1CrysralsCollected", 0);
        if (Level1Crystals == 1)
            IsLevel1CrysralsCollected = true;
        else
            IsLevel1CrysralsCollected = false;

        int Level1Fruit = PlayerPrefs.GetInt("isLevel1FruitCollected", 0);
        if (Level1Fruit == 1)
            IsLevel1FruitCollected = true;
        else
            IsLevel1FruitCollected = false;

        int Level2Crystals = PlayerPrefs.GetInt("isLevel2CrysralsCollected", 0);
        if (Level2Crystals == 1)
            IsLevel2CrysralsCollected = true;
        else
            IsLevel2CrysralsCollected = false;

        int Level2Fruit = PlayerPrefs.GetInt("isLevel2FruitCollected", 0);
        if (Level2Fruit == 1)
            IsLevel2FruitCollected = true;
        else
            IsLevel2FruitCollected = false;
    }
    void Start()
    {
        SetMusic();
    }

    private void GetWindowForSettings()
    {
        GameObject.Find("setWindow").GetComponent<setWindowM>().Display();
    }
    private void DelegativeLoadScene()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    private void SettingsForLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1")
            || SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            GameObject.Find("SetButton").GetComponent<MyButton>()
                .signalOnClick.AddListener(GetWindowForSettings);
        }
    }
    private void SetMusic()
    {
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.clip = MusicClip;
        MusicSource.priority = 10;
        MusicSource.volume = 0.15f;

        if (MusicHead.Instance.IsMusicOn)
        {
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }
    private void SetWindow()
    {
        GameObject.Find("setWindow").GetComponent<setWindowM>().Display();
    }
    public void createWinPanel()
    {
        IsLevel1Completed = true;

        PlayerPrefs.SetInt("isLevel1Completed", 1);
        PlayerPrefs.Save();
        CollectedCoins += AllLevelCoins;
        PlayerPrefs.SetInt("collectedCoins", CollectedCoins);

        GameObject Parent = UICamera.first.transform.parent.gameObject;
        GameObject Obj = NGUITools.AddChild(Parent, WinPanelPrefab);
        MasOfWin Win = Obj.GetComponent<MasOfWin>();
        Win.setCoins(AllLevelCoins);
        Win.setFruits(NumberFr, 1);
        Time.timeScale = 0;

        if (IsLevel1CrysralsCollected)
            PlayerPrefs.SetInt("isLevel1CrysralsCollected", 1);
        PlayerPrefs.Save();
    }
    public void OnRabitDeath(HeroRabit rabit)
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1")
            || SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            Lifes--;
            TotalDeath++;

            if (Lifes < 0)
            {
                PlayerPrefs.Save();
                SceneManager.LoadScene("chooselevel");
            }
            else
            {
                ForLifes.NLivesCounter.Clear(TotalDeath);
            }
        }

        rabit.transform.position = StartingPosition;
    }

    #endregion
}
  
