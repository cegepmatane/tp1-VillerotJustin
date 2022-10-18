using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    
    [Header("SFX")]
    [SerializeField] private AudioClip Gather;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<PlayerStatManagment>() != null && !col.isTrigger) {
            PlayerStatManagment.instance.Heal(1);
            AudioManager.instance.playSound(Gather);
            Destroy(gameObject);
        }
    }
}