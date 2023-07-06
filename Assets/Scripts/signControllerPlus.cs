using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signControllerPlus : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    private bool isOpen;
    private bool isValid;

    void Update() {
        if (isValid && !isOpen) {
            image1.SetActive(true);
            Invoke("OpenImage2", 0.3f);
            isOpen = true;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player"){
            isValid = true;
        }
    }

    private void OpenImage2(){
        image2.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player"){
            isValid = false;
        }
    }
}
