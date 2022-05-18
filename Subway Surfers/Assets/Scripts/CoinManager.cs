using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    private ObjectPooler objectPooler;
    public CoinSpawner[] SpawnPoint; 

    public void Start()
    {
        objectPooler = ObjectPooler.Instance;
        //SpawnPoint = FindObjectsOfType(typeof(CoinSpawner)) as Transform[];
        SpawnCoins();
    }

    public void SpawnCoins()
    {
        SpawnPoint = (CoinSpawner[]) GameObject.FindObjectsOfType (typeof(CoinSpawner));

        //spawn coins at player position
        foreach (CoinSpawner spawner in SpawnPoint)
        {
            objectPooler.SpawnFromPool("coin", spawner.transform.position, Quaternion.identity);
        }
    }
}
