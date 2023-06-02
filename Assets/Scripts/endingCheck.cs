using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingCheck : MonoBehaviour
{
    public ParticleSystem Confetti;
    public ParticleSystem Confetti1;
    public ParticleSystem Confetti2;
    public ParticleSystem Confetti3;
    public ParticleSystem Confetti4;
    private bool levelCompleted = false;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        CreateConfetties();
        if (other.gameObject.CompareTag("User") && !levelCompleted) {
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }
    }

    void CompleteLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void CreateConfetties(){
        Confetti.Play();
        Confetti1.Play();
        Confetti2.Play();
        Confetti3.Play();
        Confetti4.Play();
    }
}
