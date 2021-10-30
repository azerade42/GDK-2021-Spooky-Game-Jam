using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float swaySpeed;

    private Vector2 startPos, endPos;

    private int currentWaypointIndex = 0;
    
    private void Start()
    {
        startPos = transform.position;
        endPos = waypoints[currentWaypointIndex].transform.position;
    }

    void Update()
    {

        if (Vector2.Distance(endPos, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            startPos = waypoints[currentWaypointIndex - 1].transform.position;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                endPos = waypoints[0].transform.position;
            }
            else
            {
                endPos = waypoints[currentWaypointIndex].transform.position;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * swaySpeed);
    }
}
