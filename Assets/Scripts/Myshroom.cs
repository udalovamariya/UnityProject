public class Myshroom : Collectable
{
	protected override void OnRabitHit(HeroRabit rabbit)
	{
        if (rabbit.health == 1)
        {
            rabbit.AddHealth(1);
            HeroRabit.current.Scaletwiceformushrooms();
        }
		CollectedHide();
	}
}