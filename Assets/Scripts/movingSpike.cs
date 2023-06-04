using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingSpike : MonoBehaviour
{
    private Animator anim;
    private bool isBlinking;
    [SerializeField] private float cooldown;
    private float cooldownTimer;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

    
        if(cooldownTimer < 0) {
            isBlinking = !isBlinking;
            cooldownTimer = cooldown;
        }

        anim.SetBool("isBlinking", isBlinking);

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f) {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
