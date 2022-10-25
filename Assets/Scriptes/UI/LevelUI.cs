using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelUI : MonoBehaviour {

    // UI
    public GameObject m_PauseUI;
    public GameObject m_DeathUI;
    public GameObject m_MainMenuUI;
    
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
            if (rectTransform.gameObject.name.Equals("PauseCanva")) {
                m_PauseUI = rectTransform.gameObject;
            } else if (rectTransform.gameObject.name.Equals("DeathCanva")) {
                m_DeathUI = rectTransform.gameObject;
            }else if (rectTransform.gameObject.name.Equals("MenuUI")) {
                m_MainMenuUI = rectTransform.gameObject;
            }
            
        }
    }

}
