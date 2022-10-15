using System;
using UnityEngine;

public class DeathRow : MonoBehaviour
{
    
    [Header("Var")]
    [SerializeField] private Transform m_AdventurerTransform;
    private Transform m_DeathRowTransform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_DeathRowTransform = GetComponent<Transform>();
        m_DeathRowTransform.position = new Vector3(m_AdventurerTransform.position.x, -100, m_AdventurerTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        m_DeathRowTransform.position = new Vector3(m_AdventurerTransform.position.x, -100, m_AdventurerTransform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        PlayerMain t_Player = col.gameObject.GetComponent<PlayerMain>();

        if (t_Player)
        {
            Debug.Log("Player Lava Hit");
            t_Player.PlayerHealthManagment.TakeDamage(99999,"lava");
        }
    }
}