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

    [SerializeField] private Text cherriesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    /* void Start() {
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
    } */

    void Update() {
        
        switch(SceneManager.GetActiveScene().name) {
            case "Level1":
                n = 0;
                if(cherries == maxCherries[0]){
                    PlayerPrefs.SetInt("level1Cherries", 1);
                }
                break;
            case "Level2":
                n = 1;
                if(cherries == maxCherries[1]){
                    PlayerPrefs.SetInt("level2Cherries", 1);
                }
                break;
            case "Level3":
                n = 2;
                if(cherries == maxCherries[2]){
                    PlayerPrefs.SetInt("level3Cherries", 1);
                }
                break;
            case "Level4":
                n = 3;
                if(cherries == maxCherries[3]){
                    PlayerPrefs.SetInt("level4Cherries", 1);
                }
                break;
            case "Level5":
                n = 4;
                if(cherries == maxCherries[4]){
                    PlayerPrefs.SetInt("level5Cherries", 1);
                }
                break;
            case "Level6":
                n = 5;
                if(cherries == maxCherries[5]){
                    PlayerPrefs.SetInt("level6Cherries", 1);
                }
                break;
            case "Level7":
                n = 6;
                if(cherries == maxCherries[6]){
                    PlayerPrefs.SetInt("level7Cherries", 1);
                }
                break;
            case "Level8":
                n = 7;
                if(cherries == maxCherries[7]){
                    PlayerPrefs.SetInt("level8Cherries", 1);
                }
                break;
            case "Level9":
                n = 8;
                if(cherries == maxCherries[8]){
                    PlayerPrefs.SetInt("level9Cherries", 1);
                }
                break;
            case "Level10":
                n = 9;
                if(cherries == maxCherries[9]){
                    PlayerPrefs.SetInt("level10Cherries", 1);
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
    }
}
