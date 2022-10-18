using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    public static SpawnManager instance { get; private set; }
    
    [Header("Objects to spawn")]
    [SerializeField] private GameObject[] slimes;
    [SerializeField] private GameObject[] bonuses;


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
        
    }
    
    
    public void SpawnBonus(Transform _transform) {
        GameObject t_Instance = Instantiate((Random.value > 0.75f ? bonuses[0] : bonuses[1]), _transform.position, Quaternion.identity);
    }


}
