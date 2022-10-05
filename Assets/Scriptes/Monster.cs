using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float Speed = 3f;

    protected float last_hit;
    protected float CD = 0.5f;
    
    //Audio
    [SerializeField] protected AudioClip[] m_audioClip;
    [SerializeField] protected AudioClip m_DeathSound;
    protected AudioSource m_AS;
    protected AudioClip m_sound;

    
    private void Awake()
    {
        m_AS = GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_sound = m_audioClip[Random.Range(0, m_audioClip.Length)];
        m_AS.pitch = Random.Range(0.8f, 1.5f);
        m_AS.PlayOneShot(m_sound);
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
            t_Player.PlayerHealthManagment.TakeDamage(1,"");
            last_hit = Time.time;
        }
    }

    public void Death()
    {
        m_AS.PlayOneShot(m_DeathSound);
        Destroy(gameObject);
    }

}
