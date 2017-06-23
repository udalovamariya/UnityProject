using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseWindow : MonoBehaviour
{

    #region Fields

    public MyButton Background;
    public MyButton Close;
    public MyButton Replay;
    public MyButton Menu;
	public UI2DSprite Crystal1;
	public UI2DSprite Crystal2;
	public UI2DSprite Crystal3;
	public Sprite CrystalNotGet;

    #endregion

    void Start()
    {
        Time.timeScale = 0;
        Background.SignalOnClick.AddListener(this.OnClosePlay);
        Close.SignalOnClick.AddListener(this.OnClosePlay);
        Replay.SignalOnClick.AddListener(this.OnReplayPlay);
        Menu.SignalOnClick.AddListener(this.OnClosePlay);
		Crystal1.sprite2D = CrystalNotGet;
		Crystal2.sprite2D = CrystalNotGet;
		Crystal3.sprite2D = CrystalNotGet;
    }

    #region Methods

    private void OnReplayPlay()
    {
        Time.timeScale = 1;
        string temp = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(temp);
    }
    private void OnClosePlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainscene");
    }

    #endregion

}
