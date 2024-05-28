using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public EnemyMove enemyMove;
    public float health;
    public int bounty;
    public static Action<int> OnEnemyDied;
    [SerializeField] private Animation anim;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject deathParticles;

    public void TakeDamage(float damage) {
        health -= damage;
        
        if (!(health <= 0)) 
            return;

        StartCoroutine(Die());
    }

    private IEnumerator Die() {
        enemyMove.Stop();
        col.enabled = false;
        anim.Play("death");
        AudioManager.Instance.PlaySFX("goblinDeath", false);
        OnEnemyDied?.Invoke(bounty);
        yield return new WaitForSeconds(4f);
        AudioManager.Instance.PlaySFX("goblinVanish", false);
        deathParticles.transform.SetParent(null);
        deathParticles.SetActive(true);
        gameObject.SetActive(false);
    }
    
}
