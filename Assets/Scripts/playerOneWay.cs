using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerOneWay : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    [SerializeField] private Collider2D playerCollider;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentOneWayPlatform != null) {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("OneWayPlatform")) {
            currentOneWayPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("OneWayPlatform")) {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision() {
        BoxCollider2D platformColider = currentOneWayPlatform.GetComponent<BoxCollider2D>(); 

        Physics2D.IgnoreCollision(playerCollider, platformColider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider,platformColider, false);
    }   
}
