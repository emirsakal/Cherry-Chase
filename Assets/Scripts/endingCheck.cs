using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingCheck : MonoBehaviour
{
    private bool levelCompleted = false;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("User") && !levelCompleted) {
            levelCompleted = true;
            Invoke("CompleteLevel", 1f);
        }
    }

    void CompleteLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
