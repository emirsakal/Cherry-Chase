using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Levels Texts")]
    public Text levelsText;
    public Text backText;

    [Header("Options Texts")]
    public Text optionsText;
    public Text volumeText;
    public Text resolutionText;
    public Text fullscreenText;
    public Text languageText;
    public Text trailEffectText;
    public Text backOptionsText;
    public GameObject mainEN;
    public GameObject mainTR;
    public GameObject resetButton;
    public GameObject sifirlaButonu;

    public void FixedUpdate(){
        if(PlayerPrefs.GetInt("Language") == 0){
            levelsText.text = "BOLUMLER";
            backText.text = "GERI";
            optionsText.text = "AYARLAR";
            volumeText.text = "GENEL SES";
            resolutionText.text = "COZUNURLUK";
            fullscreenText.text = "TAM EKRAN";
            languageText.text = "DIL SECENEGI";
            trailEffectText.text = "OYUNCU IZI";
            backOptionsText.text = "GERI";
            mainEN.SetActive(false);
            resetButton.SetActive(false);
            mainTR.SetActive(true);
            sifirlaButonu.SetActive(true);
        } else if (PlayerPrefs.GetInt("Language") == 1) {
            levelsText.text = "LEVELS";
            backText.text = "BACK";
            optionsText.text = "OPTIONS";
            volumeText.text = "VOLUME";
            resolutionText.text = "RESOLUTION";
            fullscreenText.text = "FULLSCREEN";
            languageText.text = "LANGUAGE";
            trailEffectText.text = "TRAIL EFFECT";
            backOptionsText.text = "BACK";
            mainTR.SetActive(false);
            sifirlaButonu.SetActive(false);
            mainEN.SetActive(true);
            resetButton.SetActive(true);
        }
    }
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
