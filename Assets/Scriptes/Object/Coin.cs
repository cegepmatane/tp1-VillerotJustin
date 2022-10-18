using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    
    [Header("SFX")]
    [SerializeField] private AudioClip Gather;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<PlayerStatManagment>() != null) {
            PlayerStatManagment.instance.incrementCoin();
            AudioManager.instance.playSound(Gather);
            Destroy(gameObject);
        }
    }
}
