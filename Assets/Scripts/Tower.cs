using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Tower : MonoBehaviour  {
    public float range;
    public float fireRate;
    private float fireTimer;
    public GameObject bulletPrefab;
    public GameObject bulletPoint;
    private EnemyMove currentTarget;
    
    public float damage;
    public float bulletSpeed;

    protected abstract void Shoot(EnemyMove target);

    void Start() {
        fireTimer = 0;
        currentTarget = null;
    }
    
    void Update() {
        if (fireTimer > 0) {
            fireTimer -= Time.deltaTime;
            return;
        }
        
        fireTimer = fireRate;

        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, range);
        if (currentTarget != null && Array.Exists(objectsInRange, IsCurrentTarget)) {
            Vector3 targetPosition = currentTarget.transform.position;
            transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
            Shoot(currentTarget);
            return;
        }

        SetNewTarget();
        fireTimer = 0;
    }
    
    private bool IsCurrentTarget(Collider collider) {
        collider.TryGetComponent(out EnemyMove enemy);
        return enemy == currentTarget;
    }

    public void SetNewTarget() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        EnemyMove firstEnemy = null;
        float firstEnemyDistanceLeft = Mathf.Infinity;
        foreach (Collider collider in colliders) {
            if (!collider.TryGetComponent(out EnemyMove currentEnemy))
                continue;
            
            float currentEnemyDistanceLeft = Vector3.Distance(currentEnemy.transform.position, currentEnemy.nextWaypoint.transform.position);
            
            if (firstEnemy != null)
                firstEnemyDistanceLeft = Vector3.Distance(firstEnemy.transform.position, firstEnemy.nextWaypoint.transform.position);
            else {
                firstEnemy = currentEnemy;
                continue;
            }
            
            if (currentEnemy.nextWaypointIndex > firstEnemy.nextWaypointIndex || (currentEnemy.nextWaypointIndex == firstEnemy.nextWaypointIndex && currentEnemyDistanceLeft < firstEnemyDistanceLeft)) {
                firstEnemy = currentEnemy;
            }
        }
        
        currentTarget = firstEnemy;
    }
    
}
