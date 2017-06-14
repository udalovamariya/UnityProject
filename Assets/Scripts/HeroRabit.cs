using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    #region Fields

    public float WaitTime = 2f;
    float to_Wait = 0f;
	public Vector3 targetScale = Vector3.one;
    public Vector3 scale_speed = Vector3.one;
    public static HeroRabit current;
    Transform heroParent = null;
    Rigidbody2D myBody = null;
    SpriteRenderer sr;
    public int health = 1;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    public float speed = 3;
    public bool IsDead;
    public bool IsImmortal;
    public bool isForMushRooms = false;

    #endregion

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        heroParent = transform.parent;
    }
    void FixedUpdate()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;

        int LayerID = (1 << LayerMask.NameToLayer("Ground"));

        RaycastHit2D hit = Physics2D.Linecast(from, to, LayerID);
        if (hit)
            isGrounded = true;
        else
            isGrounded = false;

        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        if (value < 0)
            sr.flipX = true;
        else if (value > 0)
            sr.flipX = false;

        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        Debug.DrawLine(from, to, Color.red);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            JumpActive = true;
        }
        if (JumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                JumpTime += Time.deltaTime;
                if (JumpTime < MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                JumpActive = false;
                JumpTime = 0;
            }
        }

        if (isGrounded)
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
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            SetNewParent(this.transform, this.heroParent);
        }

        if (animator.GetBool("die"))
        {
            to_Wait -= Time.deltaTime;
            if (to_Wait <= 0)
            {
                animator.SetBool("die", false);
                LevelController.Current.OnRabitDeath(this);
                to_Wait = WaitTime;
            }
        }
    }
    void Awake()
    {
        to_Wait = WaitTime;
        current = this;
    }

    #region Private methods

    private void OnHealthChange()
    {
        if (health == 1)
        {
            transform.localScale = Vector3.one;
        }
        else if (health == 2)
        {
            transform.localScale = Vector3.one * 2;
        }
        else if (health == 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("die", true);
			//isForMushRooms = false;
			health = 1;
            StartCoroutine(Wait(1.5f));
        }
    }
    public void AddHealth(int number)
    {
        health += number;
        if (health < 0)
        {
            health = 0;
        }
        OnHealthChange();
    }
    public void Affect()
    {
        IsDead = true;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("die", true);
        StartCoroutine(Wait(1.5f));
    }
    private IEnumerator Wait(float duration)
    { 
        yield return new WaitForSeconds(duration);
        LevelController.Current.OnRabitDeath(this);
        IsDead = false;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("die", false);
    }
    public void RemoveHealth(int number)
    {
        health -= number;
        if (health < 0)
        {
            health = 0;
        }
        OnHealthChange();
    }
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
    public void Scaletwiceformushrooms()
    {
        if (!isForMushRooms)
        {
            isForMushRooms = true;
            transform.localScale += new Vector3(0.3f, 0.3f, 0.2f);
        }
    }

    #endregion
}