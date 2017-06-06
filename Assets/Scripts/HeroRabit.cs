using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        LevelController.current.setStartPosition(transform.position);
        //Зберегти стандартний батьківський GameObject
        this.heroParent = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 from = this.transform.position + Vector3.up * 0.3f;
        Vector3 to = this.transform.position + Vector3.down * 0.1f;

        int layer_id = (1 << LayerMask.NameToLayer("Ground"));

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            this.isGrounded = true;
        }
        else
        {
            this.isGrounded = false;
        }
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
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
        //Згадуємо ground check
        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }
        //  this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, this.targetScale, ref scale_speed, -1.0f);

        if (animator.GetBool("die"))
        {
            to_Wait -= Time.deltaTime;
            if (to_Wait <= 0)
            {
                animator.SetBool("die", false);
                LevelController.current.onRabitDeath(this);
                to_Wait = WaitTime;
            }
        }
    }

    void Awake()
    {
        to_Wait = WaitTime;
        current = this;
    }

    public bool isForMushRooms = false;

    public void scaletwiceformushrooms()
    {
        if (!isForMushRooms)
        {
            isForMushRooms = true;
            transform.localScale += new Vector3(0.3f, 0.3f, 0.2f);
        }
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
    public void removeHealth(int number)
    {
        this.health -= number;
        if (this.health < 0)
        {
            this.health = 0;
        }
        this.onHealthChange();
    }
    public void addHealth(int number)
    {
        this.health += number;
        if (this.health < 0)
        {
            this.health = 0;
        }
        this.onHealthChange();
    }
    void onHealthChange()
    {
        if (this.health == 1)
        {
            this.transform.localScale = Vector3.one;
        }
        else if (this.health == 2)
        {
            this.transform.localScale = Vector3.one * 2;
        }
        else if (this.health == 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("die", true);
			//isForMushRooms = false;
			health = 1;
        }
    }


}