public class Fruits : Collectable
{
    protected override void OnRabitHit(HeroRabit rabbit)
    {
        LevelController.Current.AddFruits();
        CollectedHide();
    }
}
