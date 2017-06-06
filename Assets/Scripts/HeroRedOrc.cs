using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRedOrc : HeroGreenOrc {

	// Use this for initialization
	protected override void Start () 
	{
		body = GetComponent<Rigidbody2D>();
		spriter = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		pointA = transform.position;
		destVector.y = destVector.z = 0;
		pointB = pointA + destVector;
	}

	protected override void FixedUpdate() {
		
			if (mode == Mode.Die) return;
			Vector3 rabbitPosition = HeroRabit.current.transform.position;
			Vector3 orcPosition = this.transform.position;

			if ((rabbitPosition.x > pointA.x && rabbitPosition.x < pointB.x))/* ||
      (rabbitPosition.x > pointB.x && rabbitPosition.x < pointA.x))*/
				mode = Mode.Attack;
			else if ((mode == Mode.GoToA || mode == Mode.Attack) && HasArrived(orcPosition, pointA))
				mode = Mode.GoToB;
			else if ((mode == Mode.GoToB || mode == Mode.Attack) && HasArrived(orcPosition, pointB))
				mode = Mode.GoToA;
			Run();
			if (mode == Mode.Attack && IsCloseToRabit())
			{
				if (IsDirectlyUnderRabit())
					mode = Mode.Die;
				else if (((waitTime -= Time.deltaTime) <= .0f) && transform.position.y >= rabbitPosition.y)
				{
					BumpRabit();
					waitTime = 0;
				}
			}
			StartCoroutine(Die());
	}
}
