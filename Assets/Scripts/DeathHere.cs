using UnityEngine;

public class DeathHere : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabbit = collider.GetComponent<HeroRabit>();
        if (rabbit != null)
        {
            LevelController.Current.OnRabitDeath(rabbit, true);
        }
    }
}