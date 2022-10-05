using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{

    private float Speed = 3f;
    private float m_JumpForce;

    [SerializeField] private AudioClip[] m_audioClip;
    private AudioSource m_AS;
    private AudioClip m_sound;

    private float last_hit;
    private float CD = 1.6f;
    
    private float last_jump;
    private float CD_Jump = 0.5f;

    private void Awake()
    {
        m_AS = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(-5f, 5f);
        m_sound = m_audioClip[Random.Range(0, m_audioClip.Length)];
        m_AS.pitch = Random.Range(0.8f, 1.5f);
        m_AS.PlayOneShot(m_sound);
    }

    // Update is called once per frame
    void Update()
    {
        m_JumpForce = Random.Range(0f, 500f);
        CD_Jump = Random.Range(2f, 10f);
        transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
        if ((CD_Jump + last_jump) <= Time.time)
        {
            jump();
            last_jump = Time.time;
        }
    }

    private void jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, m_JumpForce));
        m_AS.PlayOneShot(m_sound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMain t_Player = collision.gameObject.GetComponent<PlayerMain>();

        if (t_Player && ((CD + last_hit) <= Time.time))
        {
            Debug.Log("Player Hit");
            t_Player.PlayerHealthManagment.TakeDamage(1,"slimy");
            last_hit = Time.time;
        }
    }

}