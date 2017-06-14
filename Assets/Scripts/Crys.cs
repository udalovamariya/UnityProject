using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crys: Collectable
{

    private string sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite.texture.name;
    }

    protected override void OnRabitHit(HeroRabit rabit)
    {
        ForCrys.GemsCounter.GetGemThroughToken(sprite);
        this.CollectedHide();
    }

}