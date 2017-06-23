﻿using UnityEngine;

public class HeroFollow : MonoBehaviour
{

	public HeroRabit rabbit;

	void Update ()
    {
		Transform rabbit_transform = rabbit.transform;
		Transform camera_transform = transform;

		Vector3 rabbit_position = rabbit_transform.position;
		Vector3 camera_position = camera_transform.position;

		camera_position.x = rabbit_position.x;
		camera_position.y = rabbit_position.y;

		camera_transform.position = camera_position;
	}
}