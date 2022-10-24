/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour {

    private Image SlimyGooImage;

    private void Awake() {
        Transform[] childrens = GetComponentsInChildren<Transform>();
        foreach (Transform children in childrens) {
            if (children.gameObject.name.Equals("Fill")) {
                SlimyGooImage = children.GetComponent<Image>();
            }
        }
    }

    private void Start() {
        PlayerStatManagment.instance.onStamChangedCallback += UpdateSTAMHUD;
        UpdateSTAMHUD();
        
    }
    
    public void UpdateSTAMHUD() {
        float _alpha = PlayerStatManagment.instance.getStam();
        float nmbr_step = PlayerStatManagment.instance.getMaxStam();
        setTransparency((1 / nmbr_step)*(nmbr_step-_alpha));
    }

    public void setTransparency(float _alpha) {
        SlimyGooImage.color = new Color(255,255,255,_alpha);
    }
}
