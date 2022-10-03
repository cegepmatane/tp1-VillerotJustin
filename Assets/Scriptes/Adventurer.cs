using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;


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
    [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    private Boolean m_user_shte;
    const float k_Radius = .2f;                                                 // Radius of the overlap circle
    private int doubleJump = 1;
    public SpriteRenderer sprite;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask GroundLayer;
    private SpriteRenderer m_sprite;
    
    private RaycastHit2D _hit;
    public int Life = 3;
    public int MaxLife = 3;
    public int Stam = 3;
    public int MaxStam = 3;
    public GameObject DeathScreen;
    public HPUI HPUIVar;
    
    //stam regent var
    private float last_regen;
    private float CD = 2f;
    

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_ARB = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!m_userJump && (m_Grounded || doubleJump > 0) && !m_Anim.GetBool("shte")){
            if (m_Grounded)
            {
                doubleJump = 1;
            }
            m_userJump = Input.GetButtonDown("Jump");
        }
        */
        
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
        // regen stam
        RegenStam();
        
        
        sprite.color = Color.white;
        Vector2 t_feetPos = new Vector2(feetPosition.position.x, feetPosition.position.y);
        /*
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
        if (m_userJump && (m_Grounded || doubleJump > 0) && !m_Anim.GetBool("shte"))
        {
            m_Anim.SetTrigger("JumpTrigger");
            doubleJump -= 1;
            m_userJump = false;
            m_ARB.AddForce(new Vector2(0f, Jumpforce), ForceMode2D.Impulse);
        }
        */
        
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
            Stam -= 1;
            HPUIVar.ChangeStam(Stam);
            m_Anim.SetTrigger("AttackTrigger");
        }

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
