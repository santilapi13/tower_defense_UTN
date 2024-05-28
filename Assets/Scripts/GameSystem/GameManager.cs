using System;
using System.Collections;
using System.Collections.Generic;
using GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public enum TowerType {
        Rogue = 0,
        Mage = 1,
        Barbarian = 2
    }
    
    public GameObject waves;
    private int currentWaveIndex;
    private GameObject currentWave;
    [SerializeField] private int coins;
    [SerializeField] private int health;

    public HUDManager hudManager;
    public Button btnStartWave;
    public TextMeshProUGUI txtWave;
    public TextMeshProUGUI txtCoins;
    public TextMeshProUGUI txtHealth;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    
    private void OnEnable() {
        EnemyMove.OnEnemyEscaped += LoseHealth;
        Enemy.OnEnemyDied += AddMoney;
    }
    
    void Start() {
        currentWaveIndex = -1;
        txtWave.text = "";
        txtCoins.text = coins + "";
        txtHealth.text = health + "";
    }
    
    void Update() {
        if (currentWave == null) {
            return;
        }

        if (currentWave.activeSelf) {
            return;
        }

        if (currentWaveIndex == waves.transform.childCount - 1) {
            SceneController.Instance.GameWon();
            return;
        }
        
        btnStartWave.gameObject.SetActive(true);
    }

    public void StartWave() {
        currentWaveIndex++;
        txtWave.text = currentWaveIndex + 1 + " / " + waves.transform.childCount;
        currentWave = waves.transform.GetChild(currentWaveIndex).gameObject;
        currentWave.SetActive(true);
    }
    
    private void LoseHealth(int livesToTake) {
        health = (health - livesToTake > 0) ? (health  - livesToTake) : 0;
        txtHealth.text = health + "";
        if (health <= 0) {
            SceneController.Instance.GameOver();
        }
    }

    public void AddMoney(int amount) {
        coins += amount;
        txtCoins.text = coins + "";
    }

    public bool BuyTower(int cost, TowerType towerType) {
        if (cost > coins) {
            return false;
        }
        
        coins -= cost;
        txtCoins.text = coins + "";
        hudManager.BuyTower(towerType);
        return true;
    }

    public bool IsCurrentWaveActive() {
        return currentWave != null && currentWave.activeSelf;
    }

}
