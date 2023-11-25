using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float damage;
    private float speed;
    public Transform target;
    private Vector3 lastTargetPosition;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField] 
    private Collider col;

    private bool hit;

    private void Start() {
        Vector3 forceDirection = (target.position - transform.position).normalized;
        rb.AddForce(forceDirection * speed, ForceMode.Impulse);
    }
    
    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    private void LateUpdate() {
        if (!hit)
            transform.up = -rb.velocity;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Enemy enemy)) {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Tower"))
            return;
        
        rb.isKinematic = true;
        hit = true;
        col.enabled = false;
    }
    
}
