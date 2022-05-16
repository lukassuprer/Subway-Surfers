using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public GameObject coinPrefab;
    private ObjectPooler objectPooler;

    public void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        //random number if its between some numbers then run method to spawn random number of coins from position of player
        if (UnityEngine.Random.Range(0, 100) < 1)
        {
            SpawnCoins();
        }
    }
    
    public void SpawnCoins()
    {
        //spawn coins at player position
        objectPooler.SpawnFromPool("Coins", transform.position, Quaternion.identity);
    }
}
