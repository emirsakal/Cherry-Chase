using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] private AudioSource bgMusic;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void NextLevel() {
        Time.timeScale = 1f;
        Invoke ("NextLevelLoad", 1.5f);
    }
    private void NextLevelLoad() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
