using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelNumberCheck : MonoBehaviour
{
    public GameObject levelCheck;

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("User")){
            levelCheck.SetActive(false);
        }
    }
}
