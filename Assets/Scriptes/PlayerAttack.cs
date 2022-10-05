using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerMain
{
    
    [SerializeField] private Collider2D m_AttackEnableCollider;				// A collider that will be enabled when attacking
    
    // attack var
    
    // shte
    private Boolean m_user_shte;
    
    // heath managment stam
    public int Stam;

    // Update is called once per frame
    void Update()
    {
        if (!m_user_shte)
        {
            m_user_shte = Input.GetButtonDown("shte");
        }

        if (m_Anim.GetBool("shte"))
        {
            m_Anim.SetBool("Attack", Input.GetButtonDown("attack") && Stam>0);
            // Debug.Log(Input.GetButtonDown("attack")); 
        }
    }

    private void FixedUpdate()
    {
        // shte
        if (m_user_shte)
        {
            m_Anim.SetBool("shte", !m_Anim.GetBool("shte"));
            m_user_shte = false;
        }
        
        // attack
        if (m_Anim.GetBool("Attack"))
        {
            StartCoroutine(CR_Attack());
            m_Anim.SetBool("Attack", false);
            m_Anim.SetTrigger("AttackTrigger");
        }

    }
    
    IEnumerator CR_Attack()
    {
        m_AttackEnableCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        m_AttackEnableCollider.enabled = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster t_monster = collision.gameObject.GetComponent<Monster>();
        
        if (t_monster)
        {
            Debug.Log("Monster Hit");
            t_monster.Death();
        }
    }

    public void setStam(int stam)
    {
        Stam = stam;
    }
    
    
}
