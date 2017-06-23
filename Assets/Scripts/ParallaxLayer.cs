using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{

    #region Fields

    public float Slowdown = 0.5f;
	Vector3 LastPosition;

    #endregion

    void Awake()
	{
		LastPosition = Camera.main.transform.position;
	}
	void LateUpdate()
	{
		Vector3 newPosition = Camera.main.transform.position;
		Vector3 diff = newPosition - LastPosition;
		LastPosition = newPosition;
		Vector3 myPos = transform.position;

		myPos += Slowdown * diff;
		transform.position = myPos;
	}

}