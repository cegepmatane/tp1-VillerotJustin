using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float spawn_CD = 1f;
    public GameObject Prefab;
    private float monster_speed = 3f;
    private float m_LastSpawn;

    private void Start()
    {
        InvokeRepeating("Spawn", 5f, 5f);
    }

    private void Spawn()
    {
        if (m_LastSpawn + spawn_CD <= Time.time)
        {
            GameObject t_Instance = Instantiate(Prefab, transform.position, Quaternion.identity);
            m_LastSpawn = Time.time;
            monster_speed = UnityEngine.Random.Range(-5f, 5f);
            t_Instance.GetComponent<Monster>().Speed = monster_speed;

        }
        
    }

}
