using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireControl : MonoBehaviour
{
    private Animator anim;
    private bool isWorking;
    [SerializeField] private float cooldown;
    private float cooldownTimer;

    public GameObject damageFire;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

    
        if(cooldownTimer < 0) {
            isWorking = !isWorking;
            cooldownTimer = cooldown;
        }

        if(isWorking) {
            damageFire.GetComponent<BoxCollider2D>().enabled = true;
        } else {
            damageFire.GetComponent<BoxCollider2D>().enabled = false;
        }

        anim.SetBool("isWorking", isWorking);
    }
}
