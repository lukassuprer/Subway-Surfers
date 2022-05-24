using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject deathScreen;
    public UIManager UIManager;
    public GameObject player;

    public void DeadState()
    {
        Debug.Log("dead");
        UIManager.ShowDeathScreen(true);
        player.GetComponent<PlayerMovement>().enabled = false;
    }
}
