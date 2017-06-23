using System.Collections;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    #region Fields

    public float WaitTime = 2f;
    public float ToWait = 2.0f;

    public Vector3 TargetScale = Vector3.one;
    public Vector3 Scale_speed = Vector3.one;
    public static HeroRabit Current;

    Transform HeroParent = null;
    Rigidbody2D MyBody = null;
    SpriteRenderer sr;

    public int Health = 1;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    public float Speed = 3;
    public bool IsForMushRooms = false;
    public bool IsImmortal;
    public bool IsDead;

    private bool IsGrounded = false;
    private bool IsJump = false;
    private float JumpTime = 0f;

    private int rh = 0;

    public AudioClip SoundJump;
    public AudioSource SourceJump;

    public AudioClip SoundRun;
    public AudioSource SourceRun;

    public AudioClip SoundDie;
    public AudioSource SourceDie;

    #endregion

    void Start()
    {
        MyBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        HeroParent = transform.parent;

        SourceJump = gameObject.AddComponent<AudioSource>();
        SourceJump.clip = SoundJump;
        SourceRun = gameObject.AddComponent<AudioSource>();
        SourceRun.clip = SoundRun;
        SourceDie = gameObject.AddComponent<AudioSource>();
        SourceDie.clip = SoundDie;
    }
    void FixedUpdate()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;

        int layer_id = (1 << LayerMask.NameToLayer("Ground"));

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
            IsGrounded = true;
        else
            IsGrounded = false;

        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {


            Vector2 vel = MyBody.velocity;
            vel.x = value * Speed;
            MyBody.velocity = vel;
        }

        if (value < 0)
            sr.flipX = true;
        else if (value > 0)
            sr.flipX = false;

        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
            if (MusicHead.Instance.IsSound)
                SourceRun.Play();
        }
        else
        {
            animator.SetBool("run", false);
        }

        Debug.DrawLine(from, to, Color.red);


        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            IsJump = true;
            if (MusicHead.Instance.IsSound)
                SourceJump.Play();
        }
        if (IsJump)
        {


            if (Input.GetButton("Jump"))
            {
                JumpTime += Time.deltaTime;
                if (JumpTime < MaxJumpTime)
                {
                    Vector2 vel = MyBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    MyBody.velocity = vel;
                }
            }
            else
            {
                IsJump = false;
                JumpTime = 0;
            }
        }

        if (IsGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
        if (hit)
        {
            if (hit.transform != null
                && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(transform, hit.transform);
            }
        }
        else
        {
            SetNewParent(transform, HeroParent);
        }

        if (animator.GetBool("die"))
        {
            ToWait -= Time.deltaTime;
            if (ToWait <= 0)
            {
                animator.SetBool("die", false);
                //LevelController.Current.OnRabitDeath(this);
                ToWait = WaitTime;
            }
        }
    }
    void Awake()
    {
        ToWait = WaitTime;
        Current = this;
    }

    #region Methods

    public void GetUp()
    {
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            Vector2 vel = MyBody.velocity;
            vel.y += i;
            MyBody.velocity = vel;
        }
    }
    public void AddHealth(int number)
    {
        Health += number;
        if (Health < 0)
        {
            Health = 0;
        }
        OnHealthChange();
    }
    public void RemoveHealth(int number)
    {
        Health -= number;
        if (Health < 0)
        {
            Health = 0;
        }
        OnHealthChange();
    }
    private void OnHealthChange()
    {
        if (Health == 1)
        {
            transform.localScale = Vector3.one;
            Health = 0;
        }
        else if (Health == 2)
        {
            transform.localScale = Vector3.one * 2;
            Health = 0;
        }
        else if (Health == 0)
        {
               if (MusicHead.Instance.IsSound)
                SourceDie.Play();

            Animator animator = GetComponent<Animator>();
            animator.SetBool("die", true);
            Health = 1;
            StartCoroutine(Wait(ToWait));
        }
    }

    static void SetNewParent(Transform obj, Transform NewParent)
    {
        if (obj.transform.parent != NewParent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = NewParent;
            obj.transform.position = pos;
        }
    }

    public void Scaletwiceformushrooms()
    {
        if (!IsForMushRooms)
        {
            IsForMushRooms = true;
            transform.localScale += new Vector3(0.3f, 0.3f, 0.2f);
        }
    }
    private IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        LevelController.Current.OnRabitDeath(this);
    }

    #endregion

}