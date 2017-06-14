using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGreenOrc : MonoBehaviour
{
    #region Fields

    public float Speed = 2.0f;
    public Vector3 MoveBy = Vector3.one;

    public AudioClip Sound = null;
    private AudioSource Source = null;

    private Rigidbody2D MyBody = null;

    public enum Mode
    {
        GoToA,
        GoToB,
        Attack,
        Die
    }
    public Mode CurrentMode = Mode.GoToB;

    private Vector3 PointA;
    private Vector3 PointB;

    #endregion

    void Start()
    {
        MyBody = GetComponent<Rigidbody2D>();
        PointA = transform.position;

        MoveBy.y = 0;
        MoveBy.z = 0;
        PointB = PointA + MoveBy;
        Source = gameObject.AddComponent<AudioSource>();
        Source.clip = Sound;
    }
    void FixedUpdate()
    {
        SetMode();
        Run();
        StartCoroutine(Die());
    }

    #region Private methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CurrentMode != Mode.Die)
        {
            HeroRabit rabbit = collision.gameObject.GetComponent<HeroRabit>();
            if (rabbit != null)
            {
                Vector3 myPosition = transform.position;
                Vector3 rabbitPosition = HeroRabit.current.transform.position;
                CurrentMode = Mode.Attack;

                if (CurrentMode == Mode.Attack && Mathf.Abs(rabbitPosition.y - myPosition.y) < 1.2f)
                {
                    StartCoroutine(Attack(rabbit));
                }
                else if (CurrentMode == Mode.Attack && Mathf.Abs(rabbitPosition.y - myPosition.y) > 1.2f)
                {
                    CurrentMode = Mode.Die;
                }
            }
        }
    }

    private void SetMode()
    {
        Vector3 rabbitPosition = HeroRabit.current.transform.position;
        Vector3 myPosition = transform.position;

        if (CurrentMode == Mode.Die) return;
        else if (rabbitPosition.x > Mathf.Min(PointA.x, PointB.x)
                 && rabbitPosition.x < Mathf.Max(PointA.x, PointB.x)
                 && Mathf.Abs(rabbitPosition.y - myPosition.y) < 1.0f)
        {
            CurrentMode = Mode.Attack;
        }
        else if (CurrentMode == Mode.GoToA)
        {
            if (IsArrived(myPosition, PointA))
                CurrentMode = Mode.GoToB;
        }
        else if (CurrentMode == Mode.GoToB)
        {
            if (IsArrived(myPosition, PointB))
                CurrentMode = Mode.GoToA;
        }
        else CurrentMode = Mode.GoToB;
    }
    private IEnumerator Attack(HeroRabit rabbit)
    {
        Animator animator = GetComponent<Animator>();

        animator.SetBool("attack", true);

        if (MusicHead.Instance.IsSoundOn == true)
        {
            Source.Play();
        }

        rabbit.RemoveHealth(1);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("attack", false);
    }
    private IEnumerator Die()
    {
        if (CurrentMode == Mode.Die)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("die", true);

            GetComponent<BoxCollider2D>().isTrigger = true;

            if (MyBody != null) Destroy(MyBody);

            yield return new WaitForSeconds(3.0f);

            animator.SetBool("die", false);
            Destroy(gameObject);
        }
    }
    private void Run()
    {
        float value = GetDirection();
        Animator animator = GetComponent<Animator>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = MyBody.velocity;
            vel.x = value * Speed;
            MyBody.velocity = vel;
        }

        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
    private float GetDirection()
    {
        Vector3 myPosition = transform.position;
        Vector3 rabbitPosition = HeroRabit.current.transform.position;

        if (CurrentMode == Mode.Attack)
        {
            if (myPosition.x - rabbitPosition.x < -1)
            {
                return 1;
            }
            else if (myPosition.x - rabbitPosition.x > 1)
            {
                return -1;
            }
            else return 0;
        }
        else if (CurrentMode == Mode.GoToA)
        {
            return -1;
        }
        else if (CurrentMode == Mode.GoToB)
        {
            return 1;
        }
        return 0;
    }

    private bool IsArrived(Vector3 pos, Vector3 target)
    {
        target.z = 0;
        pos.z = 0;

        return Vector3.Distance(pos, target) < 0.2f;
    }

    #endregion
}