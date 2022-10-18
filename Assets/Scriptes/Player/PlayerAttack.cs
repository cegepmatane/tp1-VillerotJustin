using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : PlayerMain
{

    public static PlayerAttack instance { get; private set; }
    
    [Header("Attack var")]
    [SerializeField] private Collider2D m_AttackEnableCollider;			// A collider that will be enabled when attacking

    // attack var
    
    // shte
    private Boolean m_user_shte;

    private void Awake() {
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
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (!m_user_shte)
        {
            m_user_shte = Input.GetButtonDown("shte");
        }

        if (m_Anim.GetBool("shte"))
        {
            m_Anim.SetBool("Attack", Input.GetButtonDown("attack") && PlayerStatManagment.instance.getStam()>0);
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
            AudioManager.instance.playSound(sounds[0]);
            StartCoroutine(CR_Attack());
            PlayerStatManagment.instance.LowerStam();
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
            t_monster.Damage();
        }
    }
    
    
}
