using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class itemCollector : MonoBehaviour
{
    private int cherries = 0;

    public int[] maxCherries;
    private int n;
    private bool isLevelFinished = false;

    [SerializeField] private Text cherriesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    void Update() {
        
        

 
        switch(SceneManager.GetActiveScene().name) {
            case "Level1":
                n = 0;
                if(cherries == maxCherries[0] && isLevelFinished){
                    PlayerPrefs.SetInt("level1Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level2":
                n = 1;
                if(cherries == maxCherries[1] && isLevelFinished){
                    PlayerPrefs.SetInt("level2Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level3":
                n = 2;
                if(cherries == maxCherries[2] && isLevelFinished){
                    PlayerPrefs.SetInt("level3Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level4":
                n = 3;
                if(cherries == maxCherries[3] && isLevelFinished){
                    PlayerPrefs.SetInt("level4Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level5":
                n = 4;
                if(cherries == maxCherries[4] && isLevelFinished){
                    PlayerPrefs.SetInt("level5Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level6":
                n = 5;
                if(cherries == maxCherries[5] && isLevelFinished){
                    PlayerPrefs.SetInt("level6Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level7":
                n = 6;
                if(cherries == maxCherries[6] && isLevelFinished){
                    PlayerPrefs.SetInt("level7Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level8":
                n = 7;
                if(cherries == maxCherries[7] && isLevelFinished){
                    PlayerPrefs.SetInt("level8Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level9":
                n = 8;
                if(cherries == maxCherries[8] && isLevelFinished){
                    PlayerPrefs.SetInt("level9Cherries", 1);
                    isLevelFinished = false;
                }
                break;
            case "Level10":
                n = 9;
                if(cherries == maxCherries[9] && isLevelFinished){
                    PlayerPrefs.SetInt("level10Cherries", 1);
                    isLevelFinished = false;
                }
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Cherry")) {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries ++;
            cherriesText.text = cherries + "/" + maxCherries[n];
        }
        if (collision.gameObject.CompareTag("Finished")) {
            isLevelFinished = true;
        }
    }
}
