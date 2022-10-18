using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {
    
    public void RealoadCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerStatManagment.instance.respawn();
        PlayerStatManagment.instance.gameObject.SetActive(true);
    }

    public void ChangeSceneByName(String _name) {
        if (_name != null) {
            SceneManager.LoadScene(_name);
        }
    }

    public void EndGame() {
        Application.Quit();
    }
}
