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
        Adventurer t_Player = collision.gameObject.GetComponent<Adventurer>(); 
        
        if (t_Player && ((CD + last_hit) <= Time.time))
        {
            Debug.Log("Player Hit");
            t_Player.TakeDamage(1);
            last_hit = Time.time;
        }
    }
}