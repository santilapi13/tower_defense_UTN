using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health;
    public int bounty;
    public static Action<int> OnEnemyDied;

    public void TakeDamage(float damage) {
        health -= damage;
        
        if (!(health <= 0)) 
            return;

        OnEnemyDied?.Invoke(bounty);
        gameObject.SetActive(false);
    }
    
}
