using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public LevelUI LevelUI;

    private void Start() {
        LevelUI = LevelUI.instance;
    }

    public void ToggleDeathScreen() {
        LevelUI.m_DeathUI.SetActive(!LevelUI.m_DeathUI.activeSelf);
        ToggleStatUI();
    }
    
    public void TogglePauseScreen() {
        LevelUI.m_PauseUI.SetActive(!LevelUI.m_PauseUI.activeSelf);
        ToggleStatUI();
    }
    
    public void ToggleMainMenu() {
        LevelUI.m_MainMenuUI.SetActive(!LevelUI.m_MainMenuUI.activeSelf);
    }


    public void ToggleStatUI() {
        StatUI.instance.gameObject.SetActive(!StatUI.instance.gameObject.activeSelf);
    }

    public void ToggleGameScreen(bool val) {
        if (StatUI.instance) StatUI.instance.gameObject.SetActive(val);
        LevelUI.m_DeathUI.SetActive(false);
        LevelUI.m_PauseUI.SetActive(false);
    }
    
    public void ToggleWinScreen() {
        LevelUI.m_WinUI.SetActive(!LevelUI.m_WinUI.activeSelf);
    }
    
    
    
    public void changeListener() {
        if (PlayerStatManagment.instance) {
            AudioListener _temp = PlayerStatManagment.instance.gameObject.GetComponent<AudioListener>();
            _temp.enabled = !_temp.isActiveAndEnabled;
        }
    }
}
