using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoMenu() {
        Invoke("GoBackMenu", 0.3f);
    }

    private void GoBackMenu() {
        SceneManager.LoadScene("Menu");
    }
    
    public void PlayGame() {
        Invoke("PlayGameLoad", 0.3f);
    }

    public void QuitGame() {
        Application.Quit();
    }

    void PlayGameLoad() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LevelSelect1() {
        SceneManager.LoadScene("Level1");
    }
    public void LevelSelect2() {
        SceneManager.LoadScene("Level2");
    }
    public void LevelSelect3() {
        SceneManager.LoadScene("Level3");
    }
    public void LevelSelect4() {
        SceneManager.LoadScene("Level4");
    }
    public void LevelSelect5() {
        SceneManager.LoadScene("Level5");
    }
    public void LevelSelect6() {
        SceneManager.LoadScene("Level6");
    }
    public void LevelSelect7() {
        SceneManager.LoadScene("Level7");
    }
    public void LevelSelect8() {
        SceneManager.LoadScene("Level8");
    }
    public void LevelSelect9() {
        SceneManager.LoadScene("Level9");
    }
    public void LevelSelect10() {
        SceneManager.LoadScene("Level10");
    }
}
