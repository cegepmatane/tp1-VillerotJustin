using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPUI : MonoBehaviour
{
    public static HPUI instance { get; private set; }
    public TextMeshProUGUI HpCounter;
    public TextMeshProUGUI StamCounter;


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
    }


    public void ChangeHP(int change)
    {
        HpCounter.text = change.ToString();
    }
    public void ChangeStam(int change)
    {
        StamCounter.text = change.ToString();
    }
}
