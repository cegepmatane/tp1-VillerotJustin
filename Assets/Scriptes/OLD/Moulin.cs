using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moulin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Moulin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Zone moulin" + collision.gameObject.name);
    }
}