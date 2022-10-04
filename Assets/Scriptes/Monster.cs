using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float Speed = 3f;

    private float last_hit;
    private float CD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMain t_Player = collision.gameObject.GetComponent<PlayerMain>();
        string t_name = collision.collider.name;

        if (t_Player && ((CD + last_hit) <= Time.time))
        {
            Debug.Log("Player Hit");
            t_Player.PlayerHealthManagment.TakeDamage(1);
            last_hit = Time.time;
        }

        
        Debug.Log(t_name);

        if (t_name == "Blade") {
            Destroy(gameObject);
        }
    }

}
