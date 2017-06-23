using System;
using UnityEngine;

public class MusicHead
{

    #region Fields

    public static MusicHead Instance = new MusicHead();

    public bool IsMusic = true;
    public bool IsSound = true;

    #endregion

    MusicHead()
    {
        IsSound = Convert.ToBoolean(PlayerPrefs.GetInt("sound", 1));
        IsMusic = Convert.ToBoolean(PlayerPrefs.GetInt("music", 1));
    }

    #region Methods

    public void SetMusic(bool mode)
    {
        if (!mode)
        {
            LevelController.Current.MusicSource.Stop();
        }
        else
        {
            LevelController.Current.MusicSource.loop = true;
            LevelController.Current.MusicSource.Play();
        }

        IsMusic = mode;
    }
    public void SetSound(bool mode)
    {
        IsSound = mode;
    }

    #endregion

}