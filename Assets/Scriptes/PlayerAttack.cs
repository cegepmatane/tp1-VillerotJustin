using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerMain
{
    
    
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
            m_Anim.SetBool("Attack", false);
            m_Anim.SetTrigger("AttackTrigger");
        }

    }

    public void setStam(int stam)
    {
        Stam = stam;
    }
    
    
}
