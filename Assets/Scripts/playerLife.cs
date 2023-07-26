using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource bgMusic;
    public GameObject gameOverMenuUI;
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D playerCollider;
    [SerializeField] private Text deathText;
    public bool isDead = false;

    private int numberOfDeath;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();

        if(PlayerPrefs.GetInt("isTrailEffectOn") == 0) {
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        } else if(PlayerPrefs.GetInt("isTrailEffectOn") == 1) {
            gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
    }

    private void Update() {
        numberOfDeath = PlayerPrefs.GetInt("DeathNumber", 0);
        deathText.text = numberOfDeath + "";        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Trap")){
            isDead = true;
            bgMusic.Pause();
            deathSound.Play();
            Die();
        }
    }

    private void Die(){
        PlayerPrefs.SetInt("DeathNumber", numberOfDeath + 1);
        rb.bodyType = RigidbodyType2D.Static;
        playerCollider.enabled = !playerCollider.enabled;
        anim.SetTrigger("death");
    }

    private void RestartLevel() {
        Time.timeScale = 0f;
        gameOverMenuUI.SetActive(true);
    }
}
