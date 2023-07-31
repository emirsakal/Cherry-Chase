using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lastMenu : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    public Text thanksText;
    public Text githubText;
    public GameObject quitButton;
    public GameObject cikisYapButon;

    public Text TotalTimeText;
    private float TotalTime;

    private int NumberOfDeath;
    public Text DeathText;

    public Animator transition;
    public float transitionTime = 1f;

    void Start()
    {
        if(PlayerPrefs.GetInt("Language") == 0){
            thanksText.text = "TEBRIKLER, OYUNU BITIRDIN!";
            githubText.text = "BENI GITHUB UZERINDEN TAKIP EDEBILIRSIN:";
            quitButton.SetActive(false);
            cikisYapButon.SetActive(true);
        } else if (PlayerPrefs.GetInt("Language") == 1) {
            thanksText.text = "THANKS FOR PLAYING";
            githubText.text = "YOU CAN FOLLOW ME ON GITHUB:";
            cikisYapButon.SetActive(false);
            quitButton.SetActive(true);
        }

        if(!PlayerPrefs.HasKey("isMute")) {
            bgMusic.mute = false;
            PlayerPrefs.SetInt("isMute", 0);
        } else if(PlayerPrefs.GetInt("isMute") == 0) {
            bgMusic.mute = false;
        } else if (PlayerPrefs.GetInt("isMute") == 1) {
            bgMusic.mute = true;
        }

        TotalTime = PlayerPrefs.GetFloat("TotalTime");

        float MinutesTotal = Mathf.FloorToInt(TotalTime / 60);  
        float SecondsTotal = Mathf.FloorToInt(TotalTime % 60);

        TotalTimeText.text = string.Format("{0:0}:{1:00}", MinutesTotal, SecondsTotal);

        NumberOfDeath = PlayerPrefs.GetInt("DeathNumber", 0);
        DeathText.text = NumberOfDeath + "";
    }

    void Update()
    {
        if(!PlayerPrefs.HasKey("isMute")) {
            PlayerPrefs.SetInt("isMute", 0);
        }
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        StartCoroutine(LoadMenu(0));
    }

    IEnumerator LoadMenu(int levelIndex){
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadSceneAsync(levelIndex);


    }

    public void QuitGame() {
        Application.Quit();
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
