using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalPanel : MonoBehaviour
{
    #region Fields

    public Sprite CrystalNotGet;
    public List<Sprite> CrystalColors;
    public bool SOBRANU_VSE_KRISTALU;
    public bool SOBRANU_VSE_KRISTALU2;

    public static CrystalPanel Current;

    public List<UI2DSprite> CrystalPlace;

    Dictionary<CrystalColor, bool> ObtainedCrystal = new Dictionary<CrystalColor, bool>();

    #endregion

    void Start()
	{
		Current = this;
		ObtainedCrystal.Add (CrystalColor.Blue, false);
		ObtainedCrystal.Add (CrystalColor.Red, false);
		ObtainedCrystal.Add (CrystalColor.Green, false);
		RefreshCrystals();
    }

    #region Methods 

    public Dictionary<CrystalColor, bool> GetObtainedCrystal()
    {
        return ObtainedCrystal;
    }

    public void AddCrystal(string gemID)
    {
		if (gemID.Equals("gem-1"))
		{
			ObtainedCrystal [CrystalColor.Red] = true;
		}
		else if (gemID.Equals("gem-2"))
		{
			ObtainedCrystal [CrystalColor.Blue] = true;
		}
		else if (gemID.Equals("gem-3"))
		{
			ObtainedCrystal [CrystalColor.Green] = true;
		}

        SOBRANU_VSE_KRISTALU = false;
		SOBRANU_VSE_KRISTALU2 = false;

        if (SceneManager.GetActiveScene().name.Equals("Level1") && ObtainedCrystal[CrystalColor.Green] && ObtainedCrystal[CrystalColor.Red] && ObtainedCrystal[CrystalColor.Blue])
            SOBRANU_VSE_KRISTALU = true;
        else if (SceneManager.GetActiveScene().name.Equals("Level2") && ObtainedCrystal[CrystalColor.Green] && ObtainedCrystal[CrystalColor.Red] && ObtainedCrystal[CrystalColor.Blue])
            SOBRANU_VSE_KRISTALU2 = true;

		if (SOBRANU_VSE_KRISTALU)
			PlayerPrefs.SetInt ("SOBRANU_VSE_KRISTALU", 1);

		if (SOBRANU_VSE_KRISTALU2)
			PlayerPrefs.SetInt ("SOBRANU_VSE_KRISTALU2", 1);	

        RefreshCrystals();
    }
    public void RefreshCrystals()
    {
		UpdateCrystalColor(CrystalColor.Blue);
		UpdateCrystalColor(CrystalColor.Red);
		UpdateCrystalColor(CrystalColor.Green);
    }

    private void UpdateCrystalColor(CrystalColor color)
    {
        int crystal_id = (int)color;
		if (ObtainedCrystal.ContainsKey(color))
        {
             CrystalPlace[crystal_id].sprite2D = CrystalColors[crystal_id];
        }
        else
        {
			CrystalPlace[crystal_id].sprite2D = CrystalNotGet;
        }
    }

    #endregion
}