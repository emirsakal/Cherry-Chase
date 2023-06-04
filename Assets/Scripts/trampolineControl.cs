using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineControl : MonoBehaviour
{
    private Animator anim;
    private bool isWorking;
    private float bounce = 20f;

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
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            isWorking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("User"))
        {
            isWorking = false;
        }
    }
}
