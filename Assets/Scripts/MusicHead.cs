е общиеusing System;
using UnityEngine;

public class MusicHead
{

    public static MusicHead Instance = new MusicHead();

    
	
    

    public void SetSoundMode(bool mode)
    {
        IsSoundOn = mode;
    }

    public void SetMusicMode(bool mode)
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

        IsMusicOn = mode;
    }
	
	public bool IsMusicOn = true;
    public bool IsSoundOn = true;
	
	MusicHead()
    {
        IsSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt("sound", 1));
        IsMusicOn = Convert.ToBoolean(PlayerPrefs.GetInt("music", 1));
    }
	

}