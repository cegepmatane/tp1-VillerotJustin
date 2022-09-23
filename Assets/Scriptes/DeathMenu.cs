using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void TryAgain_Click()
    {
        Debug.Log("test");
        
        // Reload
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
