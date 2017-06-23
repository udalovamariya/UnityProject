public class Mushroom : Collectable
{
	protected override void OnRabitHit(HeroRabit rabbit)
	{
        if (rabbit.Health == 1)
        {
            rabbit.AddHealth(1);
            HeroRabit.Current.Scaletwiceformushrooms();
        }
		CollectedHide();
	}
}