public class Coins : Collectable
{
	protected override void OnRabitHit(HeroRabit rabit)
	{
		LevelController.Current.AddCoins(1);
		this.CollectedHide ();
	}
}
