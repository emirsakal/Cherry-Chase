using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signController : MonoBehaviour
{
    public GameObject image;
    private bool isOpen;
    private bool isValid;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && isValid && !isOpen) {
            image.SetActive(true);
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
