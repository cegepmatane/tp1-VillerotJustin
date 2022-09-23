﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Adventurer 
    : MonoBehaviour
{
    
    [FormerlySerializedAs("walkSpeed")] [FormerlySerializedAs("maxSpeed")] public float WalkSpeed = 5f;
    public float RunSpeed = 10f;
    public float Jumpforce = 8f;
    private Boolean m_facingRight = true;
    private Animator m_Anim;
    private Rigidbody2D m_ARB;
    private Boolean m_userJump;
    private Boolean m_Grounded;
    private int doubleJump = 1;
    public SpriteRenderer sprite;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask GroundLayer;
    
    private RaycastHit2D _hit;
    public int Life = 5;
    public GameObject DeathScreen;
    public HPUI HPUIVar;
    

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_userJump && (m_Grounded || doubleJump > 0) ){
            if (m_Grounded)
            {
                doubleJump = 1;
            }
            m_userJump = Input.GetButtonDown("Jump");
        }
    }

    private void FixedUpdate()
    {
        sprite.color = Color.white;
        Vector2 t_feetPos = new Vector2(feetPosition.position.x, feetPosition.position.y);
        m_Grounded = Physics2D.OverlapCircle(t_feetPos, 0.2f, GroundLayer.value);
        m_Anim.SetBool("Grounded", m_Grounded);
        
        // orientation
        float movement = Input.GetAxis("Horizontal");
        bool shift = Input.GetButton("shift");
        m_Anim.SetFloat("Speed", Mathf.Abs(shift ? movement*2 : movement));
        m_ARB.velocity = new Vector2(movement * ((shift && m_Grounded) ? RunSpeed : WalkSpeed), m_ARB.velocity.y);
        if (movement < 0 && m_facingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            m_facingRight = false;
        }
        else if (movement > 0 && !m_facingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            m_facingRight = true;
        }

        // jump
        if (m_userJump && (m_Grounded || doubleJump > 0))
        {
            m_Anim.SetTrigger("JumpTrigger");
            doubleJump -= 1;
            m_userJump = false;
            m_ARB.AddForce(new Vector2(0f, Jumpforce), ForceMode2D.Impulse);
        }

    }
    
    public void TakeDamage(int a_Damage){
        Life-=a_Damage;
        HPUIVar.ChangeHP(Life);
        sprite.color = Color.red;
        
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
