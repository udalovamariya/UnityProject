using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLevelCoins : MonoBehaviour
{
    void Start()
    {
        int coins = PlayerPrefs.GetInt("coins", 0);
        if (coins < 10)
        {
            GetComponent<UILabel>().text = "000" + coins;
        }
        else if (coins < 100)
        {
            GetComponent<UILabel>().text = "00" + coins;
        }
        else if (coins < 1000)
        {
            GetComponent<UILabel>().text = "0" + coins;
        }
        else if (coins < 10000)
        {
            GetComponent<UILabel>().text = "" + coins;
        }
    }

}
