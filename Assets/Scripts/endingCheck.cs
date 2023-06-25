using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endingCheck : MonoBehaviour
{
    public ParticleSystem Confetti;
    public ParticleSystem Confetti1;
    public ParticleSystem Confetti2;
    public ParticleSystem Confetti3;
    public ParticleSystem Confetti4;
    private bool levelCompleted = false;
    public int nextSceneLoad;

    [SerializeField] private AudioSource finishSound;
    [SerializeField] private AudioSource bgMusic;
    public GameObject FinishMenuUI;
    [SerializeField] private Text minutesText;
    [SerializeField] private Text secondsText;
    private float time = 0;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetFloat("level1Time", 99.99f);
        PlayerPrefs.SetFloat("level2Time", 99.99f);
        PlayerPrefs.SetFloat("level3Time", 99.99f);
        PlayerPrefs.SetFloat("level4Time", 99.99f);
        PlayerPrefs.SetFloat("level5Time", 99.99f);
        PlayerPrefs.SetFloat("level6Time", 99.99f);
        PlayerPrefs.SetFloat("level7Time", 99.99f);
        PlayerPrefs.SetFloat("level8Time", 99.99f);
        PlayerPrefs.SetFloat("level9Time", 99.99f);
        PlayerPrefs.SetFloat("level10Time", 99.99f);
    }

    void Update() {
        time += Time.deltaTime;
        float minutes = Mathf.FloorToInt(time / 60);  
        float seconds = Mathf.FloorToInt(time % 60);
        minutesText.text = string.Format("{0:0}:", minutes);
        secondsText.text = string.Format("{0:00}", seconds);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CreateConfetties();
        bgMusic.Stop();
        finishSound.Play();
        if (other.gameObject.CompareTag("User") && !levelCompleted) {
            levelCompleted = true;
            Invoke("CompleteLevel", 1f);

            if(nextSceneLoad > PlayerPrefs.GetInt("levelAt")) {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }

    void CompleteLevel() {
        Time.timeScale = 0f;
        FinishMenuUI.SetActive(true);

        switch(SceneManager.GetActiveScene().name) {
            case "Level1":
                if(time < PlayerPrefs.GetFloat("level1Time", 0)) {
                    PlayerPrefs.SetFloat("level1Time", time);
                }
                break;
            case "Level2":
                if(time < PlayerPrefs.GetFloat("level2Time", 0)) {
                    PlayerPrefs.SetFloat("level2Time", time);
                }
                break;
            case "Level3":
                if(time < PlayerPrefs.GetFloat("level3Time", 0)) {
                    PlayerPrefs.SetFloat("level3Time", time);
                }
                break;
            case "Level4":
                if(time < PlayerPrefs.GetFloat("level4Time", 0)) {
                    PlayerPrefs.SetFloat("level4Time", time);
                }
                break;
            case "Level5":
                if(time < PlayerPrefs.GetFloat("level5Time", 0)) {
                    PlayerPrefs.SetFloat("level5Time", time);
                }
                break;
            case "Level6":
                if(time < PlayerPrefs.GetFloat("level6Time", 0)) {
                    PlayerPrefs.SetFloat("level6Time", time);
                }
                break;
            case "Level7":
                if(time < PlayerPrefs.GetFloat("level7Time", 0)) {
                    PlayerPrefs.SetFloat("level7Time", time);
                }
                break;
            case "Level8":
                if(time < PlayerPrefs.GetFloat("level8Time", 0)) {
                    PlayerPrefs.SetFloat("level8Time", time);
                }
                break;
            case "Level9":
                if(time < PlayerPrefs.GetFloat("level9Time", 0)) {
                    PlayerPrefs.SetFloat("level9Time", time);
                }
                break;
            case "Level10":
                if(time < PlayerPrefs.GetFloat("level10Time", 0)) {
                    PlayerPrefs.SetFloat("level10Time", time);
                }
                break;
        }
    }

    void CreateConfetties(){
        Confetti.Play();
        Confetti1.Play();
        Confetti2.Play();
        Confetti3.Play();
        Confetti4.Play();
    }
}
