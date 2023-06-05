using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    [SerializeField] private ParticleSystem fallParticle;
    [SerializeField] private ParticleSystem fallParticle1;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals("Ground")) {
            fallParticle.Play();
            fallParticle1.Play();
        }
    }
}
