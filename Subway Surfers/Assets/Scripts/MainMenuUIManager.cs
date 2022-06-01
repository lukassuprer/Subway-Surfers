using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject leaderboardMenu;
    [SerializeField] private TextMeshProUGUI usernameInput;

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SubmitButton()
    {
        SaveManager.instance._SaveData.saves[^1].username = usernameInput.text;
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void LeaderboardButton()
    {
        mainMenu.SetActive(false);
        leaderboardMenu.SetActive(true);
        SaveManager.instance.CreateLeaderboard();
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        leaderboardMenu.SetActive(false);
    }
}