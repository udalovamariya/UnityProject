 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRedOrc : MonoBehaviour
{
    #region Fields

    public Vector3 MoveBy = Vector3.one;
    public float Speed = 2.0f;
    public float SeeOn = 9.0f;
    public float CarrotPeriod = 2.0f;
    private float TimeBefore;

    public AudioClip Sound = null;
    private AudioSource Source = null;

    private Rigidbody2D MyBody = null;

    public enum Mode
    {
        GoToA,
        GoToB,
        StandartAttack,
        CarrotAttack,
        Die
    }
    public Mode CurrentMode = Mode.GoToB;

    private Vector3 PointA;
    private Vector3 PointB;

    public GameObject PrefabCarrot;

    #endregion

    void Start()
    {
        MyBody = GetComponent<Rigidbody2D>();

        TimeBefore = CarrotPeriod;
        MoveBy.y = 0;
        MoveBy.z = 0;

        PointA = transform.position;
        PointB = PointA + MoveBy;

        Source = gameObject.AddComponent<AudioSource>();
        Source.clip = Sound;
    }
    void FixedUpdate()
    {
        SetMode();
        Run();
        CarrotAttack();
        StartCoroutine(Die());
    }

    #region Private methods

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CurrentMode != Mode.Die)
        {
            HeroRabit rabbit = collision.gameObject.GetComponent<HeroRabit>();
            if (rabbit != null)
            {
                Vector3 rabbitPosition = HeroRabit.current.transform.position;
                Vector3 myPosition = transform.position;
                CurrentMode = Mode.StandartAttack;

                if (CurrentMode == Mode.StandartAttack && Mathf.Abs(rabbitPosition.y - myPosition.y) < 1.6f)
                {
                    StartCoroutine(Attack(rabbit));
                }
                else if (CurrentMode == Mode.StandartAttack && Mathf.Abs(rabbitPosition.y - myPosition.y) > 1.6f)
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
        else if (Mathf.Abs(rabbitPosition.x - myPosition.x) < SeeOn)
        {
            CurrentMode = Mode.CarrotAttack;
        }
        else if (CurrentMode == Mode.GoToA)
        {
            if (isArrived(myPosition, PointA))
            {
                CurrentMode = Mode.GoToB;
            }
        }
        else if (CurrentMode == Mode.GoToB)
        {
            if (isArrived(myPosition, PointB))
            {
                CurrentMode = Mode.GoToA;
            }
        }
        else CurrentMode = Mode.GoToB;
    }
    private IEnumerator Attack(HeroRabit rabit)
    {
        Animator animator = GetComponent<Animator>();

        animator.SetBool("attack", true);
        rabit.RemoveHealth(1);
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
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();

        if (value < 0)
        {
            sr.flipX = false;

        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
        if (CurrentMode != Mode.CarrotAttack)
        {
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
        else animator.SetBool("run", false);
    }
    private float GetDirection()
    {
        Vector3 rabbitPosition = HeroRabit.current.transform.position;
        Vector3 myPosition = transform.position;

        if (CurrentMode == Mode.StandartAttack || CurrentMode == Mode.CarrotAttack)
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
    private bool isArrived(Vector3 pos, Vector3 target)
    {
        target.z = 0;
        pos.z = 0;

        return Vector3.Distance(pos, target) < 0.2f;
    }

    private void LaunchCarrot(float direction)
    {
        if (direction != 0)
        {
            GameObject obj = Instantiate(PrefabCarrot);
            obj.transform.position = transform.position;
            obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);

            Carrot carrot = obj.GetComponent<Carrot>();
            carrot.launch(direction);
        }
    }
    private void CarrotAttack()
    {
        if (CurrentMode == Mode.CarrotAttack && TimeBefore >= CarrotPeriod)
        {
            StartCoroutine(ThrowCarrot());
            TimeBefore = 0;
        }
        else TimeBefore += Time.deltaTime;
    }
    private IEnumerator ThrowCarrot()
    {

        Animator animator = GetComponent<Animator>();


        animator.SetBool("attack", true);
        Source.Play();
        LaunchCarrot(GetDirection());
        yield return new WaitForSeconds(0.8f);

        animator.SetBool("attack", false);
    }

    #endregion
}