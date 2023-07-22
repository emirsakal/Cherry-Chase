using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signController : MonoBehaviour
{
    public GameObject image;
    public GameObject image1;
    private bool isOpen;
    private bool isValid;

    void Update() {
        if (isValid && !isOpen && PlayerPrefs.GetInt("Language") == 0) {
            image1.SetActive(true);
            image.SetActive(false);
            isOpen = true;
        } else if (isValid && !isOpen && PlayerPrefs.GetInt("Language") == 1) {
            image.SetActive(true);
            image1.SetActive(false);
            isOpen = true;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player"){
            isValid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player"){
            isValid = false;
        }
    }
}


/* TO OPEN WITH E
        
        if (Input.GetKeyDown(KeyCode.E) && isValid && !isOpen) {
            image.SetActive(true);
            isOpen = true;
        } else if (Input.GetKeyDown(KeyCode.E) && isValid && isOpen) {
            image.SetActive(false);
            isOpen = false;
        }

        */