public class HeartPlusOneLife : Collectable
{
    protected override void OnRabitHit(HeroRabit rabbit)
    {
        CollectedHide();
        LevelController.Current.OnRabitLife(rabbit);
    }
}
