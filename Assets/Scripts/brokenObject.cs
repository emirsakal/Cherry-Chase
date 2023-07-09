using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenObject : MonoBehaviour
{   
    public GameObject broken;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("User")){
            Invoke("Destroy", 0.5f);
            Invoke("UnDestroy",3f);
        }
    }

    void Destroy(){
        broken.SetActive(false);
    }

    void UnDestroy(){
        broken.SetActive(true);
    }
}
