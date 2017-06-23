using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string SceneName;
    public static bool Locked = true;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HeroRabit>() && !(SceneName.Equals("Level2") && Locked))
        {
            SceneManager.LoadScene(SceneName);
        }
    }

}

