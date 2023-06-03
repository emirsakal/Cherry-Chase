using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource bgMusic;
    public GameObject gameOverMenuUI;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Trap")){
            bgMusic.Pause();
            deathSound.Play();
            Die();
        }
    }

    private void Die(){
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel() {
        Time.timeScale = 0f;
        gameOverMenuUI.SetActive(true);
    }
}
