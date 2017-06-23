using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    #region Fields

    public static StaticticsGame Level1;
    public static StaticticsGame Level2;

    public static bool SOBRANU_VSE_KRISTALU;
    public static bool SOBRANU_VSE_Fr;

    public static GameObject Objects;
    public static int CollectedCoins;
    public static LevelController Current;
    public static bool IsLevel1Complated;
    public static bool IsLevel2Complated;

    private int TotalDeath;
    private Vector3 StartingPosition;
    private int NumberFr = 0;

    public int Lifes = 3;
    public GameObject LosePanelPrefab;
    public int AllLevelCoins = 0;
    public MyButton PlayButton;
    public CrystalPanel CrystalPanel;
    public GameObject WinPanelPrefab;
    public static bool IsLevel1FruitCollected;

    public static bool IsLevel2FruitCollected;
    public static bool IsLevel2CrysralsCollected;
    public static int AllFruitsOnLevel1 = 11;

    public AudioSource MusicSource;
    public AudioClip MusicClip;

    public UILabel coinsLabel;
    public UILabel fruitsLabel;
    public UI2DSprite crystalSprites;

    #endregion

    void Awake()
    {
        Current = this;
        if (!(SceneManager.GetActiveScene().name.Equals("mainscene")))
        {
            Current.SetStartPosition(HeroRabit.Current.transform.position);
        }

        Objects = GameObject.Find("Objects");
        if (SceneManager.GetActiveScene().name.Equals("mainscene"))
        {
            PlayButton = GameObject.Find("PlayButton").GetComponent<MyButton>();
            PlayButton.SignalOnClick.AddListener(Play);

            GameObject.Find("PlayButton").GetComponent<MyButton>()
               .SignalOnClick.AddListener(Play);
            GameObject.Find("SetButton").GetComponent<MyButton>()
                .SignalOnClick.AddListener(SetWindow);
        }

        SettingsForLevel();
    }
    void Start()
    {
        SetMusic();
    }

    #region Methods

    private void SetWindow()
    {
        GameObject.Find("setWindow").GetComponent<SetWindowM>().Display();
    }
    private void Play()
    {
        SceneManager.LoadScene("chooselevel");
    }

    public void SetStartPosition(Vector3 pos)
    {
        StartingPosition = pos;
    }
    public void AddCoins(int i)
    {
        AllLevelCoins += i;
        SavingCoinsOfLevel.SavedMoneyAllLevel.SumCoins(AllLevelCoins);
    }
    public void AddFruits()
    {
        NumberFr += 1;
        fruitsLabel.text = NumberFr + "/" + AllFruitsOnLevel1;
    }
    private void DelegativeLoadScene()
    {
        SceneManager.LoadScene("LevelMenu");
    }


    private void SetMusic()
    {
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.clip = MusicClip;
        MusicSource.priority = 10;
        MusicSource.volume = 0.15f;

        if (MusicHead.Instance.IsMusic)
        {
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }
    public void OnRabitDeath(HeroRabit rabbit, bool DeathZone = false)
    {
        if (rabbit.IsForMushRooms)
        {
            rabbit.transform.localScale /= 2.0f;
            rabbit.IsForMushRooms = false;
            if (!DeathZone)
                return;
        }

        if (SceneManager.GetActiveScene().name.Equals("Level1")
            || SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            --Lifes;
            ++TotalDeath;

            if (MusicHead.Instance.IsSound && DeathZone)
                rabbit.SourceDie.Play();

            if (Lifes < 0)
            {
                
                GameObject parent = UICamera.first.transform.parent.gameObject;
               
                GameObject obj = NGUITools.AddChild(parent, LosePanelPrefab);
               
//                LooseWindow lose = obj.GetComponent<LooseWindow>();
                CollectedCoins += AllLevelCoins;
                PlayerPrefs.SetInt("collectedCoins", CollectedCoins);
                Time.timeScale = 0;
            }
            else
            {
                ForLifes.NLivesCounter.Clear(TotalDeath);
            }
        }

        rabbit.transform.position = StartingPosition;
    }
    public void OnRabitLife(HeroRabit rabit)
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1")
            || SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            if (Lifes != 3)
            {
                ++Lifes;
                --TotalDeath;
                ForLifes.NLivesCounter.Add(Lifes);
            }
        }
    }
    private void GetWindowForSettings()
    {
        GameObject.Find("setWindow").GetComponent<SetWindowM>().Display();
    }

    private void SettingsForLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1")
            || SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            GameObject.Find("SetButton").GetComponent<MyButton>()
                .SignalOnClick.AddListener(GetWindowForSettings);
        }
    }
    public void CreateWinPanel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            IsLevel1Complated = true;
            PlayerPrefs.SetInt("IsLevelPassed", 1);
            PlayerPrefs.Save();
        }
       else if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            IsLevel2Complated = true;
            PlayerPrefs.SetInt("IsLevelPassed2", 1);
            PlayerPrefs.Save();
        }


        GameObject parent = UICamera.first.transform.parent.gameObject;
        GameObject obj = NGUITools.AddChild(parent, WinPanelPrefab);

        obj.GetComponent<MasOfWin>().SetCoins(AllLevelCoins);
		if (SceneManager.GetActiveScene().name.Equals("Level1"))
        	obj.GetComponent<MasOfWin>().SetFruits(NumberFr, 1);

		if (SceneManager.GetActiveScene().name.Equals("Level2"))
			obj.GetComponent<MasOfWin>().SetFruits(NumberFr, 2);

        obj.GetComponent<MasOfWin>().SetCrystal(CrystalPanel.GetObtainedCrystal(), 1);

        Time.timeScale = 0;
        CollectedCoins += AllLevelCoins;
        PlayerPrefs.SetInt("collectedCoins", CollectedCoins);
    }

    #endregion

}