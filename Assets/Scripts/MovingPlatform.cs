using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region Fields

    public Vector3 MoveBy;
    public float Speed = 2f;
    public float WaitTime = 0.5f;

    Vector3 PointA;
    Vector3 PointB;

    float ToWait;

    bool GoingToA = false;

    #endregion

    void Start()
    {
        PointA = transform.position;
        PointB = PointA + MoveBy;
    }
    void Update()
    {
        Vector3 target;

        if (ToWait > 0)
        {
            ToWait -= Time.deltaTime;
            return;
        }

        if (GoingToA)
            target = PointA;
        else
            target = PointB;

        Vector3 my_pos = transform.position;
        if (IsArrived(target, my_pos))
        {
            GoingToA = !GoingToA;
            ToWait = WaitTime;
        }

        else
        {
            Vector3 destination = target - my_pos;
            float move = Speed * Time.deltaTime;
            float distance = Vector3.Distance(destination, my_pos);

            Vector3 move_vec = destination.normalized * Mathf.Min(move, distance);
            transform.position += move_vec;
        }
    }

    bool IsArrived(Vector3 pos, Vector3 target)
    {
        target.z = 0;
        pos.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }
}