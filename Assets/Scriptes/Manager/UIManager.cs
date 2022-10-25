using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {


    public void ToggleDeathScreen() {
        LevelUI.instance.m_DeathUI.SetActive(!LevelUI.instance.m_DeathUI.activeSelf);
        ToggleStatUI();
    }
    
    public void TogglePauseScreen() {
        LevelUI.instance.m_PauseUI.SetActive(!LevelUI.instance.m_PauseUI.activeSelf);
        ToggleStatUI();
    }
    
    public void ToggleMainMenu() {
        LevelUI.instance.m_MainMenuUI.SetActive(!LevelUI.instance.m_MainMenuUI.activeSelf);
    }


    public void ToggleStatUI() {
        StatUI.instance.gameObject.SetActive(!StatUI.instance.gameObject.activeSelf);
    }

    public void ToggleGameScreen(bool val) {
        if (StatUI.instance) StatUI.instance.gameObject.SetActive(val);
        LevelUI.instance.m_DeathUI.SetActive(false);
        LevelUI.instance.m_PauseUI.SetActive(false);
        
    }
    
    
    
    public void changeListener() {
        if (PlayerStatManagment.instance) {
            AudioListener _temp = PlayerStatManagment.instance.gameObject.GetComponent<AudioListener>();
            _temp.enabled = !_temp.isActiveAndEnabled;
        }
    }
}
