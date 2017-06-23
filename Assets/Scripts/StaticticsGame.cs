using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticticsGame : MonoBehaviour
{

    public Sprite GetAllFruits;
    public Sprite GetAllCrystals;

    public bool IsLevelPassed, SOBRANU_VSE_KRISTALU, SOBRANU_VSE_Fr;
    public bool IsLevelPassed2, SOBRANU_VSE_KRISTALU2, SOBRANU_VSE_Fr2;
    
    void Start()
    {
        GetInfo();
		GetInfo2();
        
        UnlockNextLevel();
    }

    private void UnlockNextLevel()
    {
        IsLevelPassed = PlayerPrefs.GetInt("IsLevelPassed", 0) == 1 ? true : false;
        if (IsLevelPassed)
        {
            Destroy(GameObject.Find("keyToLevel2"));
            Door.Locked = false;
        }
    }

    private void GetInfo()
    {
        SpriteRenderer FruitsStatusIcon = GameObject.Find("fr1")
            .GetComponent<SpriteRenderer>();
        SpriteRenderer CrystalStatusIcon = GameObject.Find("crys1")
            .GetComponent<SpriteRenderer>();
        SpriteRenderer LevelPassedIcon = GameObject.Find("done1")
            .GetComponent<SpriteRenderer>();

        SOBRANU_VSE_KRISTALU = PlayerPrefs.GetInt("SOBRANU_VSE_KRISTALU", 0) == 1 ? true : false;
        SOBRANU_VSE_Fr = PlayerPrefs.GetInt("SOBRANU_VSE_Fr", 0) == 1 ? true : false;

        if (SOBRANU_VSE_KRISTALU)
        {
            CrystalStatusIcon.sprite = GetAllCrystals;
        }

        if (SOBRANU_VSE_Fr)
        {
            FruitsStatusIcon.sprite = GetAllFruits;
        }
    }
    private void GetInfo2()
    {
        SpriteRenderer FruitsStatusIcon2 = GameObject.Find("fr2")
            .GetComponent<SpriteRenderer>();
        SpriteRenderer CrystalStatusIcon2 = GameObject.Find("crys2")
            .GetComponent<SpriteRenderer>();
        SpriteRenderer LevelPassedIcon2 = GameObject.Find("done2")
            .GetComponent<SpriteRenderer>();


        SOBRANU_VSE_KRISTALU2 = PlayerPrefs.GetInt("SOBRANU_VSE_KRISTALU2", 0) == 1 ? true : false;
        SOBRANU_VSE_Fr2 = PlayerPrefs.GetInt("SOBRANU_VSE_Fr2", 0) == 1 ? true : false;

        Debug.Log(SOBRANU_VSE_KRISTALU2);
        Debug.Log(SOBRANU_VSE_Fr2);

        if (SOBRANU_VSE_KRISTALU2)
        {
            CrystalStatusIcon2.sprite = GetAllCrystals;
        }

        if (SOBRANU_VSE_Fr2)
        {
            FruitsStatusIcon2.sprite = GetAllFruits;
        }
    }

}

