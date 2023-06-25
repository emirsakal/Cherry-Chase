using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    public GameObject[] stars;
    public GameObject[] emptyStars;
    [SerializeField] private Text Level1MinutesText;

    void Start()
    {
        // PlayerPrefs.SetInt("levelAt", 1);
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);

        float level5Time = PlayerPrefs.GetFloat("level5Time", 0.0f);
        float minutes = Mathf.FloorToInt(level5Time / 60);  
        float seconds = Mathf.FloorToInt(level5Time % 60);
        
        Level1MinutesText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

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
