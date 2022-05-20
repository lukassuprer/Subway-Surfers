using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public VolumeSettings _VolumeSettings;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PauseButton()
    {
        if (gameManager.GamePaused)
        {
            gameManager.GamePaused = false;
            _VolumeSettings.musicSlider.gameObject.SetActive(false);
            _VolumeSettings.sfxSlider.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gameManager.GamePaused = true;
            _VolumeSettings.musicSlider.gameObject.SetActive(true);
            _VolumeSettings.sfxSlider.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
