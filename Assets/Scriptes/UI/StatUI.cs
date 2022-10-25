using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUI : MonoBehaviour {

    [Header("UI")]
    public HealthBarController m_HPUI;
    public StaminaBarController m_STAMUI;
    public GoldBarController m_GOLG;
    
    public static StatUI instance { get; private set; }
        
    // instance
    private void Awake()
    {
        gameObject.SetActive(true);
        // keep object
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        } //destroy dupli
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        m_HPUI = GetComponentInChildren<HealthBarController>(true);
        m_STAMUI = GetComponentInChildren<StaminaBarController>(true);
        m_GOLG = GetComponentInChildren<GoldBarController>(true);
    }

}
