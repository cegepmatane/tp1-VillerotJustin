using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldBarController : MonoBehaviour {

    private Animator m_Anim;
    private Text m_Text;

    private void Awake() {
        m_Anim = GetComponentInChildren<Animator>();
        m_Text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        /* Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)PlayerStatManagment.instance.getMaxTotalHealth];
        heartFills = new Image[(int)PlayerStatManagment.instance.getMaxTotalHealth];*/
        
        PlayerStatManagment.instance.onGoldChangedCallback += UpdateGoldHUD;
        UpdateGoldHUD();
    }
    
    public void UpdateGoldHUD() {
        m_Anim.SetTrigger("CoinTrigger");
        m_Text.text = (PlayerStatManagment.instance.getNumberOfCoin).ToString();
    }
    
    
}
