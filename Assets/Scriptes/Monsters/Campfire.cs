using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Campfire : MonoBehaviour
{ 
    private float last_hit;
    private float CD = 1.6f;
    
    public void OnTriggerEnter2D(Collider2D collision)
    { 
        PlayerMain t_Player = collision.gameObject.GetComponent<PlayerMain>();

        if (t_Player && CD + last_hit <= Time.time && !collision.isTrigger)
        {
            Debug.Log("Player Hit");
            t_Player.PlayerHealthManagment.TakeDamage(1,"fire");
            last_hit = Time.time;
        }
    }
}