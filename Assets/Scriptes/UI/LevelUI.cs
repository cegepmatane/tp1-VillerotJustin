using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelUI : MonoBehaviour {

    // UI
    public GameObject m_PauseUI;
    public GameObject m_DeathUI;
    public GameObject m_MainMenuUI;
    public GameObject m_WinUI;
    
    public static LevelUI instance { get; private set; }
        
    // instance
    private void Awake()
    {
        gameObject.SetActive(true);
        // keep object
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        } //destroy dupli
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        foreach (RectTransform rectTransform in GetComponentsInChildren<RectTransform>(true)) {
            switch (rectTransform.gameObject.name) {
                case "PauseCanva":
                    m_PauseUI = rectTransform.gameObject;
                    break;
                case "DeathCanva":
                    m_DeathUI = rectTransform.gameObject;
                    break;
                case "MenuCanva":
                    m_MainMenuUI = rectTransform.gameObject;
                    break;
                case "WinCanva":
                    m_WinUI = rectTransform.gameObject;
                    break;
            }
            
        }
    }

}
