using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonControl : MonoBehaviour
{
    [Header("Button")]
    public GameObject ButtonOn;
    public GameObject ButtonOff;

    [Header("Door")]
    public GameObject Door;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("User")){
            Button();
        }
    }

    void Button() {
        ButtonOn.SetActive(false);
        ButtonOff.SetActive(true);
        Door.SetActive(false);
    }
}

