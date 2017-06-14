﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HeroRabit>())
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}