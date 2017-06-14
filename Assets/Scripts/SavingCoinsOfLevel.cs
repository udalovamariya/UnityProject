using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingCoinsOfLevel : MonoBehaviour {

    public static SavingCoinsOfLevel SavedMoneyAllLevel;
    private UILabel countOfCoints;

    private void Awake()
    {
        SavedMoneyAllLevel = this;
        countOfCoints = transform.GetComponent<UILabel>();
    }

   
    string num = "";
    while (n < 1000)
    {
       num += "0";
       n = n * 10;
    }
    countOfCoints.text = num + n.ToString();

}

