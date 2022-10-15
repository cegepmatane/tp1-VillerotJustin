using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [Header("UI")] 
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject statsUI;
    
    [Header("Miscelianous")]
    [SerializeField] private AudioListener m_player;
    [SerializeField] private AudioListener m_camera;

    public void ToggleDeathScreen() {
        deathScreen.SetActive(!deathScreen.activeSelf);
    }
    public void ToggleStatsUI() {
        statsUI.SetActive(!statsUI.activeSelf);
    }

    public void changeListener() {
        m_player.enabled = !m_player.isActiveAndEnabled;
        m_camera.enabled = !m_camera.isActiveAndEnabled;
    }
}
