using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGreenOrc : MonoBehaviour
{
    public enum Mode
    {
        GoToA = 0, GoToB = 1, Attack = 2, Die = 3
    }

    public float speed = 1f;
    public float runSpeed = 2.5f;
    public float hitRange = 2f;
    public Vector3 destVector = Vector3.one;

    virtual protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        pointA = transform.position;
        destVector.y = destVector.z = 0;
        pointB = pointA + destVector;
    }

    virtual protected void FixedUpdate()
    {
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

    protected float getDirection()
    {
        Vector3 rabbitPosition = HeroRabit.current.transform.position;
        Vector3 orcPosition = transform.position;
        if (mode == Mode.Attack)
        {
            return orcPosition.x - rabbitPosition.x < -1 ? 1 :
             orcPosition.x - rabbitPosition.x > 1 ? -1 : 0;
        }
        return mode == Mode.GoToA ? -1 : mode == Mode.GoToB ? 1 : 0;
    }

    protected bool HasArrived(Vector3 position, Vector3 target)
    {
        position.z = 0;
        target.z = 0;
        return Vector3.Distance(position, target) < 0.5f;
    }

    protected void Run()
    {
        float direction = this.getDirection();

        if (direction != .0f)
            spriter.flipX = direction > 0;
        if (Mathf.Abs(direction) > 0)
        {
            Vector2 vel = body.velocity;
            if (mode == Mode.Attack) vel.x = direction * runSpeed;
            else vel.x = direction * speed;
            body.velocity = vel;
        }
        if (mode == Mode.Attack)
        {
            animator.SetBool("run", Mathf.Abs(direction) > 0);
           // animator.SetBool("Walk", !(Mathf.Abs(direction) > 0));
        }
        else
        {
        //    animator.SetBool("Walk", Mathf.Abs(direction) > 0);
            animator.SetBool("run", !(Mathf.Abs(direction) > 0));
        }
    }

    
    protected bool IsCloseToRabit()
    {
        Vector3 rp = HeroRabit.current.transform.position;
        Vector3 op = this.transform.position;
        if (!HeroRabit.current.isForMushRooms)
            return Mathf.Abs(transform.position.x - HeroRabit.current.transform.position.x) < 2.5f;

        return Mathf.Abs(transform.position.x - HeroRabit.current.transform.position.x) < 3.5f;
    }


    protected bool IsDirectlyUnderRabit()
    {
		return Mathf.Abs(HeroRabit.current.transform.position.x - transform.position.x) < 1.0f
         && Mathf.Abs(HeroRabit.current.transform.position.y - this.transform.position.y) < 2.5f;
    }

    protected void BumpRabit()
    {
		//animator.SetTrigger ("Attack");
        animator.SetBool("attack", true);
        waitTime = 0.5f;
        LevelController.current.onRabitDeath(HeroRabit.current);
        animator.SetBool("attack", false);
    }

    protected IEnumerator Die()
    {
        if (mode == Mode.Die)
        {
            animator.SetBool("death", true);
            GetComponent<BoxCollider2D>().isTrigger = true;
            if (body) Destroy(body);

            yield return new WaitForSeconds(1f);
            animator.SetBool("death", false);
            Destroy(this.gameObject);
        }
    }

	protected const float DEATH_HEIGHT = 1;
	protected float waitTime;
    public Mode mode = Mode.GoToB;
	protected Rigidbody2D body;
	protected SpriteRenderer spriter;
	protected Animator animator;
    public Vector3 pointA;
    public Vector3 pointB;

}