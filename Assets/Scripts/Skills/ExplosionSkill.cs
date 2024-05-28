using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ExplosionSkill : MonoBehaviour {
    [SerializeField] protected float damage = 10;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float radius;
    [SerializeField] private bool movementEnabled = false;  
    [SerializeField] private Vector3 target ;
    [SerializeField] protected GameObject explosionParticle;

    void Update() {
        if (!movementEnabled)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void ActiveMovement(Vector3 newTarget) {
        target = newTarget;
        movementEnabled = true;
        PlayFlyingSound();
    }

    protected abstract void AffectEnemy(Enemy enemy);
    protected abstract void PlayFlyingSound();
    protected abstract void PlayExplosionSound();
    
    private void Explode() {
        explosionParticle.transform.SetParent(null);
        explosionParticle.SetActive(true);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders) {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
                AffectEnemy(enemy);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        PlayExplosionSound();
        if (other.gameObject.CompareTag("Floor")) {
            Explode();
            Destroy(gameObject);
        }
    }
}
