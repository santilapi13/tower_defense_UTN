using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
    public GameObject waypoints;
    public GameObject nextWaypoint;
    public int nextWaypointIndex;
    public static Action OnEnemyEscaped;
    public NavMeshAgent agent;
    public float speed;
    
    void Awake() {
        nextWaypoint = waypoints.transform.GetChild(0).gameObject;
        nextWaypointIndex = 0;
    }

    void OnEnable() {
        transform.LookAt(nextWaypoint.transform);
        agent.SetDestination(nextWaypoint.transform.position);
    }
    
    void Update() {
        if (agent.hasPath) return;
        
        if (nextWaypointIndex == waypoints.transform.childCount - 1) {
            OnEnemyEscaped?.Invoke();
            gameObject.SetActive(false);
            return;
        }
        
        agent.SetDestination(nextWaypoint.transform.position);
        nextWaypointIndex++;
        nextWaypoint = waypoints.transform.GetChild(nextWaypointIndex).gameObject;
        transform.LookAt(nextWaypoint.transform);
    }
    
}
