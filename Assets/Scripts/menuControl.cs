using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuControl : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] private AudioSource bgMusic;
    int isMute;
    public Text pauseText;
    public Text gameOverText;
    public Text finishText;
    public GameObject quitButton1;
    public GameObject quitButton2;
    public GameObject quitButton3;
    public GameObject cikisYapButton1;
    public GameObject cikisYapButton2;
    public GameObject cikisYapButton3;
    public playerLife PlayerLife;
    public endingCheck EndingCheck;

    public Animator transition;
    public float transitionTime = 1f;
    // private bool isDead;
    // private bool isFinished;

    void Start()
    {
        if(PlayerPrefs.GetInt("Language") == 0){
            pauseText.text = "OYUN DURDU";
            gameOverText.text = "OLDUN!";
            finishText.text = "TEBRIKLER!";
            quitButton1.SetActive(false);
            quitButton2.SetActive(false);
            quitButton3.SetActive(false);
            cikisYapButton1.SetActive(true);
            cikisYapButton2.SetActive(true);
            cikisYapButton3.SetActive(true);
        } else if (PlayerPrefs.GetInt("Language") == 1) {
            pauseText.text = "PAUSE";
            gameOverText.text = "GAME OVER";
            finishText.text = "WELL DONE!";
            cikisYapButton1.SetActive(false);
            cikisYapButton2.SetActive(false);
            cikisYapButton3.SetActive(false);
            quitButton1.SetActive(true);
            quitButton2.SetActive(true);
            quitButton3.SetActive(true);
        }

        if(!PlayerPrefs.HasKey("isMute")) {
            bgMusic.mute = false;
            PlayerPrefs.SetInt("isMute", 0);
        } else if(PlayerPrefs.GetInt("isMute") == 0) {
            bgMusic.mute = false;
        } else if (PlayerPrefs.GetInt("isMute") == 1) {
            bgMusic.mute = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerLife.isDead && !EndingCheck.isFinished) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }

        if(!PlayerPrefs.HasKey("isMute")) {
            PlayerPrefs.SetInt("isMute", 0);
        }
    }

    public void Resume() {
        bgMusic.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        bgMusic.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {

        StartCoroutine(LoadLevel(0));
    }

    public void PlayAgain() {

        StartCoroutine(LoadSameLevel());
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void NextLevel() {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    private void NextLevelLoad() {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex){
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadSceneAsync(levelIndex);


    }

    IEnumerator LoadSameLevel(){
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
        
    }
    
    public void Mute(){
        if(PlayerPrefs.GetInt("isMute") == 0) {
            bgMusic.mute = true;
            PlayerPrefs.SetInt("isMute", 1);
        } else if (PlayerPrefs.GetInt("isMute") == 1) {
            bgMusic.mute = false;
            PlayerPrefs.SetInt("isMute", 0);
        }
    }

}
