using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myshroom : Collectable
{
	protected override void OnRabitHit(HeroRabit rabit)
	{
        if (rabit.health == 1)
        {
            rabit.addHealth(1);
            HeroRabit.current.scaletwiceformushrooms();
        }
		this.CollectedHide();
	}
}