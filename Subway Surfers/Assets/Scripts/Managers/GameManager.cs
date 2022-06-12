using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GamePaused = false;
    public static GameManager Instance;
    public bool GameOver = false;
    private UIManager uiManager;
    private int lives = 3;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void DecreaseHealth()
    {
        if (GameOver)
        {
            lives = 0;
            uiManager.LivesUpdate(lives);
        }
        else
        {
            lives -= 1;
            uiManager.LivesUpdate(lives);
        }
    }
}
