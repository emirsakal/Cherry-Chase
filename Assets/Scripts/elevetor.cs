using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevetor : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    public bool didTouch = false;
    private Animator movementAnim;
    

    
    [SerializeField] private float speed = 2f;

    void Start() {
        movementAnim = GetComponent<Animator>(); 
        movementAnim.SetBool("isWorking", false);
    }
    private void Update ()
    {
        if(didTouch) {
            movementAnim.SetBool("isWorking", true);
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f) {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length) {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        } 
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("User")) {
            didTouch = true;
        }
    }
}
