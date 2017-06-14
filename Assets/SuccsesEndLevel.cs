using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccsesEndLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HeroRabit>())
        {
           
                LevelController.Current.createWinPanel();

            }
        }

    }
