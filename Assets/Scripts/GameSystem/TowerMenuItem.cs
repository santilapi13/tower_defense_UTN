using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuItem : MonoBehaviour {
    public Button button;
    public int cost;
    public GameManager.TowerType towerType;

    void Start() {
        transform.GetChild(0).TryGetComponent(out TextMeshProUGUI costText);
        costText.text = cost + "";
        button.onClick.AddListener(OnClick);
    }
    
    void OnClick() {
        if (GameManager.Instance.BuyTower(cost, towerType))
            transform.parent.gameObject.SetActive(false);
    }
}
