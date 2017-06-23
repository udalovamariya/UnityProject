
using UnityEngine;

public class SavingCoinsOfLevel : MonoBehaviour
{

    #region Fields

    public static SavingCoinsOfLevel SavedMoneyAllLevel;

    private UILabel CountOfCoints;

    #endregion

    private void Awake()
    {
        SavedMoneyAllLevel = this;
        CountOfCoints = transform.GetComponent<UILabel>();
    }

    public void SumCoins(int n)
    {
        string result = "";
        int buff = n;
        while (buff < 1000)
        {
            result += "0";
            buff *= 10;
        }
        CountOfCoints.text = result + n;
    }
}

