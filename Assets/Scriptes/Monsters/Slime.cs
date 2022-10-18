using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slime : Monster
{
    [SerializeField] private AudioClip m_LandingSound;
    [SerializeField] private AudioClip m_jumpSound;
    private AudioSource audioSource;
    
    
    
    [Header("Movement")]
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    
    // jumpVAr
    private float m_JumpForce;
    private float last_jump;
    private float CD_Jump = 0.5f;
    private bool m_JumpRight;
    private bool m_LastJumpRight;
    
    [Header("Miscellaneous")]
    [SerializeField] private int m_Color = 1;
    
    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_AS = GetComponent<AudioSource>();
        Life = 3*m_Color;
        MaxLife = 3*m_Color;
    }
    
    
    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    PlaySound(m_LandingSound);
                    m_Anim.SetTrigger("Landing");
                }
            }
        }

        m_Anim.SetBool("IsGrounded", m_Grounded);
    }
    
    void Update()
    {
        if ((CD_Jump + last_jump) <= Time.time)
        {
            jump();
            last_jump = Time.time;
        }
        
        
        // If the input is moving the player right and the player is facing left...
        if (m_JumpRight != m_LastJumpRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    
    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
		
		
    }

    private void jump()
    {
        // jump
        m_JumpForce = Random.Range(250f, 500f);
        CD_Jump = Random.Range(2f, 10f);
        m_LastJumpRight = m_JumpRight;
        m_JumpRight = Random.value >= 0.5;
        m_Anim.SetTrigger("Jump");
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(m_JumpRight ? -500f : 500f, m_JumpForce));
        PlaySound(m_jumpSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMain t_Player = collision.gameObject.GetComponent<PlayerMain>();

        if (t_Player && CD + last_hit <= Time.time && !collision.collider.isTrigger)
        {
            Debug.Log("Player Hit");
            t_Player.playerStatManagment.TakeDamage(1,"slimy");
            last_hit = Time.time;
        }
    }


}