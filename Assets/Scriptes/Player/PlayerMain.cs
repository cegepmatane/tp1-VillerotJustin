using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMain : MonoBehaviour
{
   
    
    [Header("Subscripts")]
    public PlayerAttack PlayerAttack;
    [FormerlySerializedAs("PlayerHealthManagment")] public PlayerStatManagment playerStatManagment;
    public PlayerMovement PlayerMovement;
    
    // global var
    protected Animator m_Anim;
    protected Rigidbody2D m_ARB;
    protected SpriteRenderer m_sprite;
    
    [Header("SFX")]
    [SerializeField] protected AudioClip[] sounds;
    
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
