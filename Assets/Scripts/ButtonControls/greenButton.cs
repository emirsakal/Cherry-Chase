using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenButton : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClick;

    [Header("Button")]
    public GameObject ButtonOn;
    public GameObject ButtonOff;
    public GameObject openRed;
    public GameObject openYellow;

    [Header("Other")]
    public GameObject beforeImage;
    public GameObject image;
    public GameObject noMovement;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("User")){
            Button();
            Invoke("OpenButtons", 8f);
        }
    }

    void Button() {
        // buttonClick.Play();
        ButtonOn.SetActive(false);
        ButtonOff.SetActive(false);
        beforeImage.SetActive(false);
        image.SetActive(true);
        noMovement.SetActive(true);
    }

    void OpenButtons() {
        ButtonOn.SetActive(false);
        ButtonOff.SetActive(false);
        openRed.SetActive(true);
        openYellow.SetActive(true);
        noMovement.SetActive(false);
    }
}

