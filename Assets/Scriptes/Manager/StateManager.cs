using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {
    
    public UIManager UIManager;

    private void Awake() {
        UIManager = GetComponent<UIManager>();
    }

    private void Update() {
        if (Input.GetButtonDown("Escape") && !SceneManager.GetActiveScene().name.Equals("Main_Menu")) PauseGame();
    }
    
    public void RealoadCurrentScene() {
        if (UIManager.LevelUI.m_WinUI.activeSelf) UIManager.ToggleWinScreen();
        if (UIManager.LevelUI.m_DeathUI.activeSelf) UIManager.ToggleDeathScreen();
        Time.timeScale = 1;
        PlayerStatManagment.instance.respawn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeSceneByName(String _name) {
        switch ((1 * (LevelUI.instance.m_DeathUI.activeSelf ? 2 : 1) * (LevelUI.instance.m_PauseUI.activeSelf ? 3 : 1) * (LevelUI.instance.m_MainMenuUI.activeSelf ? 10 : 1))) {
            case 2:
                UIManager.ToggleDeathScreen();
                break;
            case 3:
                UIManager.TogglePauseScreen();
                break;
            case 10:
                UIManager.ToggleMainMenu();
                break;
            
        }
        if (_name != null) {
            if (_name.Equals("Main_Menu")) {
                UIManager.ToggleMainMenu();
                UIManager.ToggleGameScreen(false);
            } else {
                UIManager.ToggleGameScreen(true);
            }
            if (PlayerStatManagment.instance) PlayerStatManagment.instance.respawn();
            UIManager.changeListener();
            SceneManager.LoadScene(_name);
            Time.timeScale = 1;
        }
    }

    public void PauseGame() {
        LevelManager.instance.UIManager.TogglePauseScreen();
        Time.timeScale = (Time.timeScale + 1)%2;
    }

    public void EndGame() {
        Application.Quit();
    }
}
