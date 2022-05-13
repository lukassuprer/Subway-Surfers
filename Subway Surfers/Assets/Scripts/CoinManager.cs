using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public GameObject coinPrefab;
    
    void Start()
    {
        coinCount = 0;
        for(int i = 0; i < ObjectPool.instance.amountToPool; i++)
        {
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        coinPrefab = ObjectPool.instance.GetPooledObject();
        if (coinPrefab != null)
        {
            coinPrefab.transform.position = transform.position;
            coinPrefab.transform.rotation = transform.rotation;
            coinPrefab.SetActive(true);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
    }
}
