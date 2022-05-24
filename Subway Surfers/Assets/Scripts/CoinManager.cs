using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public UIManager UIManager;

    private void Update()
    {
        UIManager.ScoreUpdate(coinCount);
    }
}
