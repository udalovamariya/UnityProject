﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable 
{
	public void onRabbitEnter (HeroRabit rabit)
	{
		rabit.removeHealth (1);
		this.CollectedHide ();
	}

	float my_direction = 0;
	public float Speed = 2;

	public void launch(float direction) 
	{
		this.my_direction = direction;

		if (direction < 0) 
		{
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}

		StartCoroutine (destroyLater());
	}

	IEnumerator destroyLater() 
	{
		yield return new WaitForSeconds (3f);

		Destroy(this.gameObject);
	}


	void Update () 
	{
		Vector3 pos = this.transform.position;
		pos.x += Time.deltaTime * my_direction * Speed;
		this.transform.position = pos;
	}
}