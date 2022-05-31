using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject deathScreen;
    public UIManager UIManager;
    public GameObject player;
    public SaveManager SaveManager;
    public CoinManager CoinManager;

    public void DeadState()
    {  
        SaveScore();
        //Debug.Log("dead");
        UIManager.ShowDeathScreen(true);
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    
    private void SaveScore()
    {
        SaveManager._SaveData.saves[^1].score = CoinManager.coinCount;
        SaveManager.SaveData();
    }
}
