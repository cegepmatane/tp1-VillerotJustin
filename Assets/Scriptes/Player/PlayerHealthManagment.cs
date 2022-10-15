using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;


public class PlayerHealthManagment : PlayerMain
{

    [Header("UI var")]
    public GameObject DeathScreen;
    public HPUI HPUIVar;
    
    // life var
    protected int Life = 5;
    protected int MaxLife = 5;
    
    // stam  var
    protected int Stam = 3;
    protected int tmp_Stamp = 3;
    protected int MaxStam = 3;
    private float last_regen;
    private float CD = 2f;
    
    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack.setStam(Stam);
        if (m_Anim.GetBool("Attack"))
        {
            Debug.Log("Stam bf attack " +Stam);
            Debug.Log("tmp_Stamp " + tmp_Stamp);
            Stam -= 1;
            Debug.Log("Stam af attack2 " +Stam);
            Debug.Log("stam Hm2 " + Stam);
            Debug.Log("tmp_Stamp2 " + tmp_Stamp);
            UpdateUIStam();
            tmp_Stamp = Stam;
        }
    }

    private void FixedUpdate()
    {
        // regen stam
        RegenStam();
    }

    private void RegenStam()
    {
        if (((CD + last_regen) <= Time.time) && Stam < MaxStam)
        {
            last_regen = Time.time;
            Stam += 1;
            HPUIVar.ChangeStam(Stam);
        }
    }

    public void UpdateUIStam()
    {
        Debug.Log("Attack change Stam");
        HPUIVar.ChangeStam(Stam);
    }
    
    
    
    public void TakeDamage(int a_Damage, String a_effect){
        Debug.Log("TakeDamage : " + a_Damage + " " + a_effect);
        Debug.Log("Life : " + Life);
        Life-=a_Damage;
        HPUIVar.ChangeHP(Life);
        Debug.Log("Life af : " + Life);
        
        AudioManager.instance.playSound(sounds[UnityEngine.Random.Range(0,1)]);
        
        switch (a_effect)
        { 
            case "": 
                StartCoroutine(CR_Flash());
                break; 
            case "slimy":
               StartCoroutine(CR_Slimy());
               break;
            case "fire":
                StartCoroutine(CR_Flame());
               break;
        }

        if(Life<=0){
            Die();
        }
    }
    
    IEnumerator CR_Flash()
    {
        for (int i = 0; i < 4; i++)
        {
            m_sprite.color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            m_sprite.color = Color.red;
            yield return new WaitForSeconds(0.2f);
        }
        m_sprite.color = Color.white;
    }
    IEnumerator CR_Flame()
    {
        for (int i = 0; i < 4; i++)
        {
            m_sprite.color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            m_sprite.color = Color.yellow;
            yield return new WaitForSeconds(0.2f);
        }
        m_sprite.color = Color.yellow;
        yield return new WaitForSeconds(3f);
        m_sprite.color = Color.white;
    }
    
    IEnumerator CR_Slimy()
    {
        PlayerMovement.PlayerController2D.m_MovementSmoothing = 0.5f;
        for (int i = 0; i < 4; i++)
        {
            m_sprite.color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            m_sprite.color = Color.green;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(3f);
        m_sprite.color = Color.white;
        PlayerMovement.PlayerController2D.m_MovementSmoothing = 0.05f;
    }
    
    public void Heal(int a_Heal){
        Life+=a_Heal;
        if (Life > MaxLife)
        {
            Life = MaxLife;
        }
        HPUIVar.ChangeHP(Life);
        
        if(Life<=0){
            Die();
        }
    }

    private void Die(){
        AudioManager.instance.playSound(sounds[2]);
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }
}
