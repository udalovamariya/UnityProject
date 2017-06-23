using UnityEngine;

public class Crys: Collectable
{

    private string sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite.texture.name;
    }

    protected override void OnRabitHit(HeroRabit rabbit)
    {   
        ForCrys.CrysCounter.GetCrysThroughToken(sprite);
		CollectedHide();
		CrystalPanel.Current.AddCrystal(sprite);
    }

}