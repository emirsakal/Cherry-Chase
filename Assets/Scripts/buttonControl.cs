using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonControl : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Red Button")]
    public GameObject RedButtonOn;
    public GameObject RedButtonOff;

    [Header("Blue Button")]
    public GameObject BlueButtonOn;
    public GameObject BlueButtonOff;

    [Header("Green Button")]
    public GameObject GreenButtonOn;
    public GameObject GreenButtonOff;

    [Header("Yellow Button")]
    public GameObject YellowButtonOn;
    public GameObject YellowButtonOff;

    [Header("Doors")]
    public GameObject RedDoor;
    public GameObject BlueDoor;
    public GameObject GreenDoor;
    public GameObject YellowDoor;

    public int doesItWork = 0;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("RedButton")){
            RedButton();
        }
        if (collision.gameObject.CompareTag("BlueButton")){
            BlueButton();
        }
        if (collision.gameObject.CompareTag("GreenButton")){
            GreenButton();
        }
        if (collision.gameObject.CompareTag("YellowButton")){
            YellowButton();
        }
    }

    void RedButton() {
        doesItWork++;
        RedButtonOn.SetActive(false);
        RedButtonOff.SetActive(true);
        RedDoor.SetActive(false);
    }

    void BlueButton() {
        doesItWork++;
        BlueButtonOn.SetActive(false);
        BlueButtonOff.SetActive(true);
        BlueDoor.SetActive(false);
    }

    void YellowButton() {
        doesItWork++;
        YellowButtonOn.SetActive(false);
        YellowButtonOff.SetActive(true);
        YellowDoor.SetActive(false);
    }

    void GreenButton() {
        doesItWork++;
        GreenButtonOn.SetActive(false);
        GreenButtonOff.SetActive(true);
        GreenDoor.SetActive(false);
    }
}
