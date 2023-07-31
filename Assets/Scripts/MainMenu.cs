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

    public Animator transition;
    public float transitionTime = 1f;

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
        StartCoroutine(LoadLevel("Level1"));
    }
    public void LevelSelect2() {
        StartCoroutine(LoadLevel("Level2"));
    }
    public void LevelSelect3() {
        StartCoroutine(LoadLevel("Level3"));
    }
    public void LevelSelect4() {
        StartCoroutine(LoadLevel("Level4"));
    }
    public void LevelSelect5() {
        StartCoroutine(LoadLevel("Level5"));
    }
    public void LevelSelect6() {
        StartCoroutine(LoadLevel("Level6"));
    }
    public void LevelSelect7() {
        StartCoroutine(LoadLevel("Level7"));
    }
    public void LevelSelect8() {
        StartCoroutine(LoadLevel("Level8"));
    }
    public void LevelSelect9() {
        StartCoroutine(LoadLevel("Level9"));
    }
    public void LevelSelect10() {
        StartCoroutine(LoadLevel("Level10"));
    }

    IEnumerator LoadLevel(string name){
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadSceneAsync(name);
    
    }
}
