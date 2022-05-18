using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private StartOfTile startOfTile;
    private ObjectPooler objectPooler;
    public int TilesCrossed;
    private Vector3 positionToSpawn;
    public List<GameObject> ActiveTiles = new List<GameObject>();

    private void Start()
    {
        startOfTile = FindObjectOfType<StartOfTile>();
        objectPooler = ObjectPooler.Instance;
        SpawnTile();
        SpawnTile();
        SpawnTile();
    }

    private void Update()
    {
        if (TilesCrossed >= 2)
        {
            SpawnTile();
            SpawnTile();
            if (ActiveTiles.Count > 3)
            {
                for (int i = 0; i < 2; i++)
                {
                    ActiveTiles[0].SetActive(false);
                    ActiveTiles.RemoveAt(0);
                }
            }
        }
    }

    private void SpawnTile()
    {
        GameObject newTile = objectPooler.SpawnFromPool("tile", transform.position, Quaternion.identity);
        ActiveTiles.Add(newTile);
        transform.position +=
            Vector3.forward * objectPooler.ObjectToSpawn.transform.GetChild(0).GetChild(0).localScale.z;
        //Spawn at start 1 tile then move manager to that pos and then add + 100 on z and then spawn another ....
        TilesCrossed = 0;
    }
}