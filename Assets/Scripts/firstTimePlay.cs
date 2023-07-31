using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstTimePlay : MonoBehaviour
{
    public int isFirstTime = 0;

    public void Start(){
        // PlayerPrefs.SetInt("resetFirstTime", 0);
        isFirstTime = PlayerPrefs.GetInt("resetOnce", 0);

        if(isFirstTime == 0) {
            PlayerPrefs.SetFloat("level1Time", 999999.0f);
            PlayerPrefs.SetFloat("level2Time", 999999.0f);
            PlayerPrefs.SetFloat("level3Time", 999999.0f);
            PlayerPrefs.SetFloat("level4Time", 999999.0f);
            PlayerPrefs.SetFloat("level5Time", 999999.0f);
            PlayerPrefs.SetFloat("level6Time", 999999.0f);
            PlayerPrefs.SetFloat("level7Time", 999999.0f);
            PlayerPrefs.SetFloat("level8Time", 999999.0f);
            PlayerPrefs.SetFloat("level9Time", 999999.0f);
            PlayerPrefs.SetFloat("level10Time", 999999.0f);
            PlayerPrefs.SetInt("level1Cherries",0);
            PlayerPrefs.SetInt("level2Cherries",0);
            PlayerPrefs.SetInt("level3Cherries",0);
            PlayerPrefs.SetInt("level4Cherries",0);
            PlayerPrefs.SetInt("level5Cherries",0);
            PlayerPrefs.SetInt("level6Cherries",0);
            PlayerPrefs.SetInt("level7Cherries",0);
            PlayerPrefs.SetInt("level8Cherries",0);
            PlayerPrefs.SetInt("level9Cherries",0);
            PlayerPrefs.SetInt("level10Cherries",0);
            PlayerPrefs.SetInt("DeathNumber", 0);
            PlayerPrefs.SetInt("levelAt", 1);
            PlayerPrefs.SetInt("resetOnce", 1);
        }
    }
}
