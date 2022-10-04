using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;


public class PlayerHealthManagment : PlayerMain
{

    // UI var
    public GameObject DeathScreen;
    public HPUI HPUIVar;
    
    // life var
    private RaycastHit2D _hit;
    protected int Life = 3;
    protected int MaxLife = 3;
    
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
    
    
    
    public void TakeDamage(int a_Damage){
        Life-=a_Damage;
        HPUIVar.ChangeHP(Life);

        StartCoroutine(CR_Flash());
        
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
        // show menu
        m_Anim.SetBool("Dead", true);
        DeathScreen.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
