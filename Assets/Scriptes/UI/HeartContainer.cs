using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour {
    
    private Image heartFill;

    private void Awake() {
        heartFill = transform.Find("HeartFill").GetComponent<Image>();
    }

    public void fill(float filling) {
        heartFill.fillAmount = filling;
    }
}
