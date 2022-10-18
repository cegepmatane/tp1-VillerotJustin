using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using UnityEngine.PlayerLoop;


public class PlayerStatManagment : PlayerMain
{

    public static PlayerStatManagment instance { get; private set; }
    private Vector3 m_spawnPosition;
    
    // life var
    protected int Life = 5;
    protected int MaxLife = 10;
    
    // stam  var
    protected int Stam = 10;
    protected int MaxStam = 10;
    private float last_regen;
    private float CD = 2f;
    
    // coin var
    protected int numberOfCoin;

    public void incrementCoin() {
        numberOfCoin++;
    }
    
    
    private void Awake()
    {
        gameObject.SetActive(true);
        // keep object
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        } //destroy dupli
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_spawnPosition = this.transform.position;
        MaxLife = 10;
        Life = MaxLife;
        HPUI.instance.ChangeHP(Life);
    }

    public void LowerStam() {
        Stam--;
    }

    private void Update() {
        HPUI.instance.ChangeStam(Stam);
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
            HPUI.instance.ChangeStam(Stam);
        }
    }

    public int getStam() {
        return Stam;
    }
    
    
    
    public void TakeDamage(int a_Damage, String a_effect){
        Debug.Log("TakeDamage : " + a_Damage + " " + a_effect);
        Debug.Log("Life : " + Life);
        Life-=a_Damage;
        HPUI.instance.ChangeHP(Life);
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
        HPUI.instance.ChangeHP(Life);
    }

    private void Die(){
        AudioManager.instance.playSound(sounds[2]);
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    public void respawn() {
        transform.position = m_spawnPosition;
        MaxLife = 10;
        Life = MaxLife;
    }
}
