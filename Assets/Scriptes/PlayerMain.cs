using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    // sub scripts
    public PlayerAttack PlayerAttack;
    public PlayerHealthManagment PlayerHealthManagment;
    public PlayerMovement PlayerMovement;
    
    // global var
    protected Animator m_Anim;
    protected Rigidbody2D m_ARB;
    protected SpriteRenderer m_sprite;
    
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
        
    }
    
    

    
}
