using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textControl : MonoBehaviour
{
    public GameObject image;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("User")) {
            image.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("User")){
            image.SetActive(false);
        }
    }
}
