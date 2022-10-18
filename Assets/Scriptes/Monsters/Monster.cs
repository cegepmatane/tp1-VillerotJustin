using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    // global var
    protected Animator m_Anim;
    protected Rigidbody2D m_ARB;
    protected SpriteRenderer m_sprite;
    
    public float Speed = 3f;

    
    
    [Header("SFX")]
    [SerializeField] protected AudioClip m_DeathSound;
    [SerializeField] protected AudioClip m_HurtSound;
    protected AudioSource m_AS;
    protected AudioClip m_sound;
    
    
    // life var
    protected int Life = 5;
    protected int MaxLife = 5;
    
    /* stam  var
    protected int Stam = 3;
    protected int tmp_Stamp = 3;
    protected int MaxStam = 3;
    private float last_regen;
    private float CD_regen = 2f;
    */
    
    //attack var
    protected float last_hit;
    protected float CD = 0.5f;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMain t_Player = collision.gameObject.GetComponent<PlayerMain>();

        if (t_Player && CD + last_hit <= Time.time && !collision.collider.isTrigger)
        {
            Debug.Log("Player Hit");
            t_Player.playerStatManagment.TakeDamage(1,"");
            last_hit = Time.time;
        }
    }
    
    public void Damage()
    {
        Life--;
        if (Life == 0)
        {
            Death();
        }
        PlaySound(m_HurtSound);
        m_Anim.SetTrigger("Hurt");
    }
    
    private void Death()
    {
        PlaySound(m_DeathSound);
        GetComponent<CapsuleCollider2D>().enabled = false;
        m_Anim.SetBool("Death", true);
        SpawnManager.instance.SpawnBonus(transform);
        StartCoroutine(CR_Death());
    }
    
    protected void PlaySound(AudioClip _sound)
    {
        m_AS.PlayOneShot(_sound);
    }

    IEnumerator CR_Death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
