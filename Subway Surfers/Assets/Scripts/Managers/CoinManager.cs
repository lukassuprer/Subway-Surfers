using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CoinManager : MonoBehaviour
{
    public float coinCount;
    public UIManager UIManager;
    [SerializeField]private float scorePerSecond = 1f;

    private void Update()
    {
        if(GameManager.Instance.GameOver)
        {
            return;
        }
        if (!GameManager.Instance.GamePaused)
        {
            coinCount += scorePerSecond * Time.deltaTime;
        }
        UIManager.ScoreUpdate(coinCount);
    }
}
