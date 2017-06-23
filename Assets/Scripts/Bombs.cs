public class Bombs : Collectable
{
    protected override void OnRabitHit(HeroRabit rabbit)
    {
        CollectedHide();
        rabbit.RemoveHealth(1);
    }
}
