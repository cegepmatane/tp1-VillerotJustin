using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerMain
{
    public PlayerController2D PlayerController2D;

    // movement var
    private float horizontalInput = 0f;
    protected float WalkSpeed = 40f;
    protected float RunSpeed =80f;
    protected Boolean crouch;
    
    // jump var
    private Boolean jump;

    [Header("Position Check")]
    [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    const float k_Radius = .2f;
    [SerializeField] private Transform feetPosition;

    
    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        PlayerController2D = GetComponent<PlayerController2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!jump && !m_Anim.GetBool("shte")){
            jump = Input.GetButtonDown("Jump");
        }
        
        if (Input.GetButtonDown("croush"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("croush"))
        {
            crouch = false;
        }
        
        bool shift = Input.GetButton("shift");
        horizontalInput = Input.GetAxis("Horizontal") * ((shift && !crouch) ? RunSpeed : WalkSpeed);

        
        
    }

    private void FixedUpdate()
    {
        m_Anim.SetFloat("Speed",  Mathf.Abs(horizontalInput/20));
        PlayerController2D.Move(horizontalInput*Time.fixedDeltaTime, crouch, jump, m_Anim.GetBool("shte"));
        jump = false;
    }
}
