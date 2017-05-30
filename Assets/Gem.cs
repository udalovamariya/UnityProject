using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        this.CollectedHide();
    }
}