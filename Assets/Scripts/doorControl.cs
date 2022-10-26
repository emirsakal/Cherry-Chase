using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{
    public GameObject sign;
    public GameObject border;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            sign.SetActive(true);
            border.SetActive(true);
        }
    }
}
