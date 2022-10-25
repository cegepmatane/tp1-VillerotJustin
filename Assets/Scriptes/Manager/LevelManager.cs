using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public UIManager UIManager;

    private void Awake() {
        if (instance == null ) {
            instance = this;;
            DontDestroyOnLoad(this);
        } else
        Destroy(gameObject);
        UIManager = GetComponent<UIManager>();
    }

    public void GameOver() {
        if (UIManager != null) {
            UIManager.ToggleDeathScreen();
        }
    }
}
