using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
    public int livesToTake = 1;
    public GameObject waypoints;
    public GameObject nextWaypoint;
    public int nextWaypointIndex;
    public static Action<int> OnEnemyEscaped;
    public NavMeshAgent agent;
    public float speed;
    [SerializeField] private bool isFrozen = false;
    [SerializeField] private GameObject freezeEffect;
    [SerializeField] private Animation anim;
    
    void Awake() {
        nextWaypoint = waypoints.transform.GetChild(0).gameObject;
        nextWaypointIndex = 0;
    }

    void OnEnable() {
        transform.LookAt(nextWaypoint.transform);
        agent.SetDestination(nextWaypoint.transform.position);
    }
    
    void Update() {
        if (!agent.enabled || agent.hasPath) return;
        
        if (nextWaypointIndex == waypoints.transform.childCount - 1) {
            OnEnemyEscaped?.Invoke(livesToTake);
            gameObject.SetActive(false);
            return;
        }

        agent.SetDestination(nextWaypoint.transform.position);
        nextWaypointIndex++;
        nextWaypoint = waypoints.transform.GetChild(nextWaypointIndex).gameObject;
        transform.LookAt(nextWaypoint.transform);
    }
    
    public void Freeze(float duration) {
        if (isFrozen)
            return;
        
        isFrozen = true;
        //freezeEffect.SetActive(true);
        anim.Play("idle");
        StartCoroutine(FreezeCoroutine(duration));
    }
    
    private IEnumerator FreezeCoroutine(float duration) {
        float originalSpeed = agent.speed;
        agent.speed = 0;
        agent.enabled = false;
        yield return new WaitForSeconds(duration);
        if (enabled) {
            agent.enabled = true;
            agent.SetDestination(waypoints.transform.GetChild(nextWaypointIndex - 1).position);
            agent.speed = originalSpeed;
            isFrozen = false;
            anim.Play("run");
        }
        //freezeEffect.SetActive(false);
    }

    public void Stop() {
        agent.enabled = false;
        enabled = false;
    }
    
}
