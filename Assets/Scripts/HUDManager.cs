using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HUDManager : MonoBehaviour {
    public Tower selectedTower;
    public GameObject selectedSlot;
    public GameObject buyTowerMenu;

    private void Update() {
        if (!Input.GetMouseButtonDown(0) || Input.mousePosition.y <= 220) 
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.CompareTag("Tower")) {
            for (int i = 0 ; i < hit.transform.childCount ; i++) {
                if (!hit.transform.GetChild(i).gameObject.activeSelf) continue;
                
                selectedTower = hit.transform.GetChild(i).GetComponent<Tower>();
                selectedSlot = null;
                buyTowerMenu.SetActive(false);
                return;
            }
            buyTowerMenu.SetActive(true);
            selectedSlot = hit.collider.gameObject;
            selectedTower = null;
            return;
        }
        
        buyTowerMenu.SetActive(false);
        selectedSlot = null;
        selectedTower = null;
    }
    
    private void OnDrawGizmos() {
        if (selectedTower == null)
            return;
        
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Less;
        Vector3 circlePosition = selectedTower.transform.position;
        circlePosition.y = 1.01f; 
        
        Handles.color = Color.white;
        Handles.DrawWireDisc(circlePosition, Vector3.up, selectedTower.range);
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
    }

    public void BuyTower(GameManager.TowerType towerType) {
        selectedSlot.transform.GetChild((int) towerType).gameObject.SetActive(true);
        selectedSlot = null;
        selectedTower = null;
    }

}
