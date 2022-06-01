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
        SaveScore();
        //Debug.Log("dead");
        UIManager.ShowDeathScreen(true);
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    
    private void SaveScore()
    {
        SaveManager.instance._SaveData.saves[^1].score = CoinManager.coinCount;
        //SaveManager.instance.SaveData();
    }
}
