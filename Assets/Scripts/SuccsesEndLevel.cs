using UnityEngine;

public class SuccsesEndLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HeroRabit>())
        {
            LevelController.Current.CreateWinPanel();
        }
    }
}
