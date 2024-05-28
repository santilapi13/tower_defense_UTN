using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public float timeBetweenEnemies = 1f;
    private float timer;
    private int lastEnemyIndex;

    private void OnEnable() {
        lastEnemyIndex = 0;
        timer = 0f;
    }
    
    void Update() {
        if (lastEnemyIndex == transform.childCount) {
            foreach (Transform child in transform) {
                if (child.gameObject.activeSelf) {
                    return;
                }
            }
            gameObject.SetActive(false);
        }
        
        if (timer > 0) {
            timer -= Time.deltaTime;
            return;
        }

        transform.GetChild(lastEnemyIndex).gameObject.SetActive(true);
        lastEnemyIndex++;
        timer = timeBetweenEnemies;
    }
    
}
