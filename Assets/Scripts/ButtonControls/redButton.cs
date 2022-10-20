using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redButton : MonoBehaviour
{
    [Header("Button")]
    public GameObject ButtonOn;
    public GameObject ButtonOff;

    [Header("Distances")]
    public float distanceXB = 40f;
    public float distanceYB = 1f;


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("RedButton")){
            transform.position = new Vector2(transform.position.x+distanceXB,transform.position.y+distanceYB);
        }
    }
}
