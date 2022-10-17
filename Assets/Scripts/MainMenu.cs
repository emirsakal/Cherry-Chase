using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void PlayGame() {
        Invoke("PlayGameLoad", 0.3f);
    }

    public void QuitGame() {
        Application.Quit();
    }

    void PlayGameLoad() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
