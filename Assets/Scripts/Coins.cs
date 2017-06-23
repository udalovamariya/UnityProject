public class Coins : Collectable
{
	protected override void OnRabitHit (HeroRabit rabbit)
	{
		LevelController.Current.AddCoins (1);
		CollectedHide ();
	}
}
