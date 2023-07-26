using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    public GameObject[] stars;
    public GameObject[] emptyStars;
    public Text[] levelTimers;
    [SerializeField] Text totalTimeText;
    // [SerializeField] Text levelNumberText;
    [SerializeField] Text deathText;
    public float totalTime = 0;
    public int levelNumber = 0;

    void Start()
    {
        // PlayerPrefs.SetInt("levelAt", 1);
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        deathText.text = PlayerPrefs.GetInt("DeathNumber", 0) + "";        


        // Level 1
        float level1Time = PlayerPrefs.GetFloat("level1Time", 0.0f);
        if(level1Time > 999998.0f) { level1Time = 0.0f;} else { totalTime += level1Time; levelNumber += 1;}
        float minutes1 = Mathf.FloorToInt(level1Time / 60);  
        float seconds1 = Mathf.FloorToInt(level1Time % 60);
        // Level 2
        float level2Time = PlayerPrefs.GetFloat("level2Time", 0.0f);
        if(level2Time > 999998.0f) { level2Time = 0.0f;} else { totalTime += level2Time; levelNumber += 1;}
        float minutes2 = Mathf.FloorToInt(level2Time / 60);  
        float seconds2 = Mathf.FloorToInt(level2Time % 60);
        //
        float level3Time = PlayerPrefs.GetFloat("level3Time", 0.0f);
        if(level3Time > 999998.0f) { level3Time = 0.0f;} else { totalTime += level3Time; levelNumber += 1;}
        float minutes3 = Mathf.FloorToInt(level3Time / 60);  
        float seconds3 = Mathf.FloorToInt(level3Time % 60);
        // Level 4
        float level4Time = PlayerPrefs.GetFloat("level4Time", 0.0f);
        if(level4Time > 999998.0f) { level4Time = 0.0f;} else { totalTime += level4Time; levelNumber += 1;}
        float minutes4 = Mathf.FloorToInt(level4Time / 60);  
        float seconds4 = Mathf.FloorToInt(level4Time % 60);
        // Level 5
        float level5Time = PlayerPrefs.GetFloat("level5Time", 0.0f);
        if(level5Time > 999998.0f) { level5Time = 0.0f;} else { totalTime += level5Time; levelNumber += 1;}
        float minutes5 = Mathf.FloorToInt(level5Time / 60);  
        float seconds5 = Mathf.FloorToInt(level5Time % 60);
        // Level 6
        float level6Time = PlayerPrefs.GetFloat("level6Time", 0.0f);
        if(level6Time > 999998.0f) { level6Time = 0.0f;} else { totalTime += level6Time; levelNumber += 1;}
        float minutes6 = Mathf.FloorToInt(level6Time / 60);  
        float seconds6 = Mathf.FloorToInt(level6Time % 60);
        // Level 7
        float level7Time = PlayerPrefs.GetFloat("level7Time", 0.0f);
        if(level7Time > 999998.0f) { level7Time = 0.0f;} else { totalTime += level7Time; levelNumber += 1;}
        float minutes7 = Mathf.FloorToInt(level7Time / 60);  
        float seconds7 = Mathf.FloorToInt(level7Time % 60);
        // Level 8
        float level8Time = PlayerPrefs.GetFloat("level8Time", 0.0f);
        if(level8Time > 999998.0f) { level8Time = 0.0f;} else { totalTime += level8Time; levelNumber += 1;}
        float minutes8 = Mathf.FloorToInt(level8Time / 60);  
        float seconds8 = Mathf.FloorToInt(level8Time % 60);
        // Level 9
        float level9Time = PlayerPrefs.GetFloat("level9Time", 0.0f);
        if(level9Time > 999998.0f) { level9Time = 0.0f;} else { totalTime += level9Time; levelNumber += 1;}
        float minutes9 = Mathf.FloorToInt(level9Time / 60);  
        float seconds9 = Mathf.FloorToInt(level9Time % 60);
        // Level 5
        float level10Time = PlayerPrefs.GetFloat("level10Time", 0.0f);
        if(level10Time > 999998.0f) { level10Time = 0.0f;} else { totalTime += level10Time; levelNumber += 1;}
        float minutes10 = Mathf.FloorToInt(level10Time / 60);  
        float seconds10 = Mathf.FloorToInt(level10Time % 60);
        
        PlayerPrefs.SetFloat("TotalTime", totalTime);
        float minutesTotal = Mathf.FloorToInt(totalTime / 60);  
        float secondsTotal = Mathf.FloorToInt(totalTime % 60);

        levelTimers[0].text = string.Format("{0:0}:{1:00}", minutes1, seconds1);
        levelTimers[1].text = string.Format("{0:0}:{1:00}", minutes2, seconds2);
        levelTimers[2].text = string.Format("{0:0}:{1:00}", minutes3, seconds3);
        levelTimers[3].text = string.Format("{0:0}:{1:00}", minutes4, seconds4);
        levelTimers[4].text = string.Format("{0:0}:{1:00}", minutes5, seconds5);
        levelTimers[5].text = string.Format("{0:0}:{1:00}", minutes6, seconds6);
        levelTimers[6].text = string.Format("{0:0}:{1:00}", minutes7, seconds7);
        levelTimers[7].text = string.Format("{0:0}:{1:00}", minutes8, seconds8);
        levelTimers[8].text = string.Format("{0:0}:{1:00}", minutes9, seconds9);
        levelTimers[9].text = string.Format("{0:0}:{1:00}", minutes10, seconds10);

        totalTimeText.text = string.Format("{0:0}:{1:00}", minutesTotal, secondsTotal);
        // levelNumberText.text = levelNumber + "/10";
        
        for(int i = 0; i < lvlButtons.Length; i++) {
            if (i + 1 > levelAt) {
                lvlButtons[i].interactable = false;
            }
        }
        
        if (PlayerPrefs.GetInt("level1Cherries") == 1) {
            stars[0].SetActive(true);
            emptyStars[0].SetActive(false);
        } else {
            stars[0].SetActive(false);
            emptyStars[0].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level2Cherries") == 1) {
            stars[1].SetActive(true);
            emptyStars[1].SetActive(false);
        } else {
            stars[1].SetActive(false);
            emptyStars[1].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level3Cherries") == 1) {
            stars[2].SetActive(true);
            emptyStars[2].SetActive(false);
        } else {
            stars[2].SetActive(false);
            emptyStars[2].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level4Cherries") == 1) {
            stars[3].SetActive(true);
            emptyStars[3].SetActive(false);
        } else {
            stars[3].SetActive(false);
            emptyStars[3].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level5Cherries") == 1) {
            stars[4].SetActive(true);
            emptyStars[4].SetActive(false);
        } else {
            stars[4].SetActive(false);
            emptyStars[4].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level6Cherries") == 1) {
            stars[5].SetActive(true);
            emptyStars[5].SetActive(false);
        } else {
            stars[5].SetActive(false);
            emptyStars[5].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level7Cherries") == 1) {
            stars[6].SetActive(true);
            emptyStars[6].SetActive(false);
        } else {
            stars[6].SetActive(false);
            emptyStars[6].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level8Cherries") == 1) {
            stars[7].SetActive(true);
            emptyStars[7].SetActive(false);
        } else {
            stars[7].SetActive(false);
            emptyStars[7].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level9Cherries") == 1) {
            stars[8].SetActive(true);
            emptyStars[8].SetActive(false);
        } else {
            stars[8].SetActive(false);
            emptyStars[8].SetActive(true);
        }

        if (PlayerPrefs.GetInt("level10Cherries") == 1) {
            stars[9].SetActive(true);
            emptyStars[9].SetActive(false);
        } else {
            stars[9].SetActive(false);
            emptyStars[9].SetActive(true);
        }
        
    }

}
