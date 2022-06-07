using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject deathScreen;
    public UIManager UIManager;
    public GameObject player;
    public CoinManager CoinManager;

    public void DeadState()
    {  
        GameManager.Instance.GameOver = true;
        SaveScore();
        //Debug.Log("dead");
        UIManager.ShowDeathScreen(true);
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    
    private void SaveScore()
    {
        if (CoinManager.coinCount > SaveManager.instance._SaveData.saves[^1].score)
        {
            SaveManager.instance._SaveData.saves[^1].score = (int)CoinManager.coinCount;
            SaveManager.instance.SaveData();
        }
    }
}
