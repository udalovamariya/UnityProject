using UnityEngine;

public class Collectable : MonoBehaviour
{
	protected virtual void OnRabitHit(HeroRabit rabit)
    {

    }
	private void OnTriggerEnter2D(Collider2D collider)
    {
			HeroRabit rabbit = collider.GetComponent<HeroRabit>();
			if (rabbit != null)
            {
				OnRabitHit (rabbit);
		    }
	}
	public void CollectedHide()
    {
		Destroy(gameObject);
	}
}