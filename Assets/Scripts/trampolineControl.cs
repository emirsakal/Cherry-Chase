using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineControl : MonoBehaviour
{
    private Animator anim;
    private bool isWorking;
    public Animator animPlayer;
    private enum MovementState { idle, running, jumping, falling, sliding, doubleJumping }
    [SerializeField] private float bounce = 20f;
    [SerializeField] private AudioSource trampolineSoundEffect;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    
    void Update()
    {
        anim.SetBool("isWorking", isWorking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("User"))
        {
            trampolineSoundEffect.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * bounce); // Old bounce system collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            isWorking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("User"))
        {
            animPlayer.SetInteger("state", (int)MovementState.jumping);
            isWorking = false;
        }
    }
}
