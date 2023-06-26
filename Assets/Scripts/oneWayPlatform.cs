using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneWayPlatform : MonoBehaviour
{
    public Collider2D colliderX;
    public float waitTime;
    private bool canDo;

    void Start() {
        colliderX = GetComponent<Collider2D>();
    }

    void Update(){
        if(waitTime > 0f) {
            waitTime -= Time.deltaTime;
        }
        if(canDo && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) {
            colliderX.enabled = false;
            waitTime = 0.3f;
        } else if (waitTime <= 0.1f){
            colliderX.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("User")) {
            canDo = true;
        } else {
            canDo = false;
        }
    }
}
