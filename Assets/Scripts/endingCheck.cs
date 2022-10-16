using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingCheck : MonoBehaviour
{
    public GameObject endingMenu;
    public GameObject pauseMenu;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("User")) {
                endingMenu.SetActive(true);
                pauseMenu.SetActive(false);
            }
    }
}
