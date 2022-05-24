using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public VolumeSettings _VolumeSettings;
    private GameManager gameManager;
    public GameObject _DeathScreen;
    public GameObject _PauseMenu;
    private VolumeSettings volumeSettings;
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        volumeSettings = FindObjectOfType<VolumeSettings>();
        ShowDeathScreen(false);
    }
    
    public void ScoreUpdate(int score)
    {
        ScoreText.text = "Score: " + score;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        volumeSettings.SaveVolume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ShowDeathScreen(false);
    }

    public void ShowDeathScreen(bool showMenu)
    {
        _DeathScreen.SetActive(showMenu);
    }

    public void PauseButton()
    {
        if (gameManager.GamePaused)
        {
            gameManager.GamePaused = false;
            _PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gameManager.GamePaused = true;
            _PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
