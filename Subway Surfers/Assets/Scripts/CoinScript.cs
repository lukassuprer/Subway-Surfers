using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private CoinManager coinManager;
    private AudioManager audioManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        rb.AddTorque(0, 1, 0, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioManager.PlayCoinSound();
            gameObject.SetActive(false);
            coinManager.coinCount++;
        }
    }
}
