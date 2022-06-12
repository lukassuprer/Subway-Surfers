using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject player;
    [SerializeField] private CoinManager coinManager;

    public void DeadState()
    {
        GameManager.Instance.GameOver = true;
        GameManager.Instance.DecreaseHealth();
        SaveScore();
        uiManager.ShowDeathScreen(true);
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    private void SaveScore()
    {
        if (coinManager.CoinCount > SaveManager.Instance.SaveDataArray.saves[^1].score)
        {
            SaveManager.Instance.SaveDataArray.saves[^1].score = (int) coinManager.CoinCount;
            SaveManager.Instance.SaveData();
        }
    }
}