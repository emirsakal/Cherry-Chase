using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signControllerPlus : MonoBehaviour
{
    public GameObject image;
    public GameObject button;
    private bool isOpen;
    private bool isValid;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && isValid && !isOpen) {
            image.SetActive(true);
            button.SetActive(true);
            isOpen = true;
        } else if (Input.GetKeyDown(KeyCode.E) && isValid && isOpen) {
            image.SetActive(false);
            isOpen = false;
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
