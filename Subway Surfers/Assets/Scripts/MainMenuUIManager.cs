using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]private GameObject mainMenu;
    [SerializeField]private GameObject optionsMenu;
    [SerializeField]private GameObject leaderboardMenu;
    [SerializeField]private SaveManager saveManager;
    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitButton()
    {
        Application.Quit();
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
        saveManager.CreateLeaderboard();
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        leaderboardMenu.SetActive(false);
    }
}
