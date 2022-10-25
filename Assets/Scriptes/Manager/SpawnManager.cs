using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour {
    
    public static SpawnManager instance { get; private set; }
    
    [Header("Objects to spawn")]
    [SerializeField] private GameObject[] slimes;
    [SerializeField] private GameObject[] bonuses;
    
    [Header("Spawn Var")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] public float CD_Spawn;
    protected float last_Spawn;


    void Awake()
    {
        
        // keep object
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        } //destroy dupli
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        if (CD_Spawn != 0) {
            SpawnSlime(spawnPoints[Random.Range(0, spawnPoints.Length-1)]);
            last_Spawn = Time.time;
        }

    }

    private void Update() {
        if (CD_Spawn + last_Spawn <= Time.time && CD_Spawn != 0) {
            SpawnSlime(spawnPoints[Random.Range(0, spawnPoints.Length)]);
            SpawnSlime(spawnPoints[Random.Range(0, spawnPoints.Length)]);
            last_Spawn = Time.time;
        }
        
        if (Time.time % 100 == 0) {
            CD_Spawn *= 0.9f;
        }
    }


    public void SpawnBonus(Transform _transform) {
        GameObject t_Instance = Instantiate((Random.Range(1f,100f) < 75f ? bonuses[0] : bonuses[1]), _transform.position, Quaternion.identity);
    }

    public void SpawnSlime(Transform _transform) {
        float _temp = Random.Range(1f,100f);
        if (_temp <= 50f) {
            GameObject t_Instance = Instantiate(slimes[0], _transform.position, Quaternion.identity);
        } else if (50f < _temp && _temp < 80f) {
            GameObject t_Instance = Instantiate(slimes[1], _transform.position, Quaternion.identity);
        } else if (_temp >= 80f) {
            GameObject t_Instance = Instantiate(slimes[2], _transform.position, Quaternion.identity);
        }
    }


}
