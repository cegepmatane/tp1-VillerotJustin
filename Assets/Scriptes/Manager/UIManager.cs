using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [Header("UI")] 
    [SerializeField] private GameObject deathScreen;
    
    [Header("Miscelianous")]
    [SerializeField] private AudioListener m_player;
    public AudioListener m_camera;
    
    

    private void Start() {
        m_player = PlayerStatManagment.instance.gameObject.GetComponent<AudioListener>();
    }
    
    

    public void ToggleDeathScreen() {
        deathScreen.SetActive(!deathScreen.activeSelf);
    }
    public void changeListener() {
        m_player.enabled = !m_player.isActiveAndEnabled;
        m_camera.enabled = !m_camera.isActiveAndEnabled;
    }
}
