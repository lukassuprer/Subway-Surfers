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
    [SerializeField]private List<GameObject> activeTiles = new List<GameObject>();
    private int numberOfTiles = 3;
    private bool gameStarted = false;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        objectPooler.SpawnFromPool("startTile", transform.position, Quaternion.identity);
        transform.position +=
            Vector3.forward * objectPooler.ObjectToSpawn.transform.GetChild(0).GetChild(0).localScale.z;
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    private void Update()
    {
        if (TilesCrossed == 1 && gameStarted == false)
        {
            gameStarted = true;
            TilesCrossed = 0;
        }
        if (TilesCrossed >= 2)
        {
            SpawnTile();
            SpawnTile();
            if (activeTiles.Count > 3)
            {
                for (int i = 0; i < 2; i++)
                {
                    activeTiles[0].SetActive(false);
                    activeTiles.RemoveAt(0);
                }
            }
        }
    }

    private void SpawnTile()
    {
        GameObject newTile = objectPooler.SpawnFromPool("tile", transform.position, Quaternion.identity);
        activeTiles.Add(newTile);
        transform.position +=
            Vector3.forward * objectPooler.ObjectToSpawn.transform.GetChild(0).GetChild(0).localScale.z;
        //Spawn at start 1 tile then move manager to that pos and then add + 100 on z and then spawn another ....
        TilesCrossed = 0;
    }
}