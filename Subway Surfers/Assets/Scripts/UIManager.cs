using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager; 
    [SerializeField]private GameObject deathScreen;
    [SerializeField]private GameObject pauseMenu;
    private VolumeSettings volumeSettings;
    [SerializeField]private TextMeshProUGUI scoreText;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        volumeSettings = FindObjectOfType<VolumeSettings>();
        ShowDeathScreen(false);
    }
    
    public void ScoreUpdate(float score)
    {
        scoreText.text = "Score: " + Mathf.Round(score);
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
        deathScreen.SetActive(showMenu);
    }

    public void PauseButton()
    {
        if (gameManager.GamePaused)
        {
            gameManager.GamePaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gameManager.GamePaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
