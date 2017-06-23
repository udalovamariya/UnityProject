using System.Collections;
using UnityEngine;

public class Carrot : Collectable
{

    #region Fields

    public float lifeTime = 5.0f;
    public Vector3 Speed = new Vector3(2.0f, 0.0f, 0.0f);

    private float Direction = 1.0f;

    public static uint deaths = 0;

    #endregion

    void Awake()
    {
        StartCoroutine(DestroyLater());
    }

    #region Methods

    public void Launch(float Direction)
    {
        this.Direction = Direction;
    }

    IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        if (Direction != 0)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (Direction < 0)
            {
                sr.flipX = true;
            }
            else if (Direction > 0)
            {
                sr.flipX = false;
            }

            if (Mathf.Abs(Direction) > 0)
            {
                transform.position += Direction * Speed * Time.deltaTime;
            }
        }
    }

    protected override void OnRabitHit(HeroRabit rabbit)
    {
        CollectedHide();
		if (!HeroRabit.Current.GetComponent<Animator>().GetBool("die"))
		{
			rabbit.RemoveHealth(1);
        }
    }

    #endregion

}