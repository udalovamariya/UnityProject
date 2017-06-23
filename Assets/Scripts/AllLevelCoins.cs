using UnityEngine;

public class AllLevelCoins : MonoBehaviour
{
    void Start()
    {
        int collectedCoins = PlayerPrefs.GetInt("collectedCoins", 0);
        if (collectedCoins == 0)
        {
            GetComponent<UILabel>().text = "0000";
            return;
        }

        string result = "";
        int buff = collectedCoins;
        while (buff < 1000)
        {
            result += "0";
            buff *= 10;
        }
        GetComponent<UILabel>().text = result + collectedCoins.ToString();
    }
}
