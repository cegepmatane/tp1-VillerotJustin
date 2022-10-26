using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_text : MonoBehaviour {
    private Vector3 m_PlayerPos, m_selfPos;
    private float distance;
    private Text textBox;

    [Header("Text")] 
    [SerializeField] private String message = "";


    // Start is called before the first frame update
    void Awake() {
        m_selfPos = transform.position;
        textBox = GetComponentInChildren<Text>(true);
    }

    // Update is called once per frame
    void Update() {
        m_PlayerPos = PlayerStatManagment.instance.gameObject.transform.position;
        distance = (m_PlayerPos - m_selfPos).magnitude;
        if (distance < 10) {
            if (!GetComponentInChildren<Canvas>(true).gameObject.activeSelf) {
                GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
                StartCoroutine(CR_Show_Text(message, textBox));
            }
        }
        if (distance > 10) {
            GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);
            if (textBox.text != "") {
                textBox.text = "";
            }
        }
    }
    
    IEnumerator CR_Show_Text(String _text, Text text) {
        text.text = "";
        foreach (Char _char in _text) {
            text.text += _char;
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    
}
