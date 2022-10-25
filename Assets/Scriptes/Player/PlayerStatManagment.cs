using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using UnityEngine.PlayerLoop;


public class PlayerStatManagment : PlayerMain
{

    public static PlayerStatManagment instance { get; private set; }
    private Vector3 m_spawnPosition;

    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;
    
    [FormerlySerializedAs("health")]
    [Header("Health")]
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;
    public float getHealth { get { return health; } }
    public float getMaxHealth { get { return maxHealth; } }
    public float getMaxTotalHealth { get { return maxTotalHealth; } }
    
    // stam  var
    public delegate void OnStamChangedDelegate();
    public OnStamChangedDelegate onStamChangedCallback;
    protected int Stam = 10;
    protected int MaxStam = 10;
    private float last_regen;
    private float CD = 2f;
    
    // coin var
    public delegate void OnGoldChangedCallback();
    public OnGoldChangedCallback onGoldChangedCallback;
    private int numberOfCoin;
    public float getNumberOfCoin { get { return numberOfCoin; } }

    public void incrementCoin() {
        numberOfCoin++;
        ClampCoin();
    }

    private void ClampCoin() {
        if (onGoldChangedCallback != null)
            onGoldChangedCallback.Invoke();
    }
    
    // instance
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
        m_spawnPosition = transform.position;
        maxHealth = 10;
        health = maxHealth;
        maxTotalHealth = 10;
    }
    
    // Heatlth stuff
    
    
    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }
    
    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;
            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }
    
    void ClampHealth()
    {
        if (health <= 0) {
            StartCoroutine(CR_Death());
        }
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
    
    // Damage stuff
    
    public void TakeDamage(float a_Damage, String a_effect){
        health-=a_Damage;
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
        ClampHealth();
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
    
    // Stam stuff
    
    public void LowerStam() {
        Stam--;
        ClampStam();
    }

    private void Update()
    {
        // regen stam
        RegenStam();
    }

    private void RegenStam()
    {
        if (((CD + last_regen) <= Time.time))
        {
            last_regen = Time.time;
            Stam += 1;
            ClampStam();
        }
    }

    public int getStam() {
        return Stam;
    }
    public int getMaxStam() {
        return MaxStam;
    }
    
    void ClampStam() {
        Stam = Mathf.Clamp(Stam, 0, MaxStam);
        if (onStamChangedCallback != null)
            onStamChangedCallback.Invoke();
    }
    

    // death stuff
    
    IEnumerator CR_Death() {
        m_Anim.SetTrigger("DeathTrigger");
        AudioManager.instance.playSound(sounds[2]);
        yield return new WaitForSeconds(1f);
        m_Anim.SetBool("Dead",true);
        yield return new WaitForSeconds(3f);
        LevelManager.instance.GameOver();
        Time.timeScale = 0;
    }

    public bool isNotDead() {
        return !m_Anim.GetBool("Dead");
    }

    public void respawn() {
        Time.timeScale = 1;
        transform.position = m_spawnPosition;
        Debug.Log("respawn");
        Debug.Log(m_spawnPosition.ToString());
        maxHealth = 10;
        health = maxHealth;
        numberOfCoin = 0;
        Stam = MaxStam;
        m_Anim.SetBool("Dead",false);
        ClampHealth();
        ClampStam();
        ClampCoin();
    }
   
    
    
    
}
