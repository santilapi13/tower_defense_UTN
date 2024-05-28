using UnityEditor;
using UnityEngine;

namespace GameSystem
{
    public class HUDManager : MonoBehaviour {
        public Tower selectedTower;
        public GameObject selectedSlot;
        public GameObject buyTowerMenu;
        public Skill selectedSkill;
        public static HUDManager Instance;
        [SerializeField] private Skill[] skillsList; 
    
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        private void Update() {
            if (!Input.GetMouseButtonDown(0) || Input.mousePosition.y <= Screen.height * 0.25f) 
                return;
        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.CompareTag("Tower")) {
                if (selectedSkill != null)
                    selectedSkill.Deselect();
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
        
            if (selectedSkill != null)
                selectedSkill.PerformSkill(hit.point);
        
            buyTowerMenu.SetActive(false);
            selectedSlot = null;
            selectedTower = null;
        }
    
        #if UNITY_EDITOR
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
        #endif

        public void BuyTower(GameManager.TowerType towerType) {
            selectedSlot.transform.GetChild((int) towerType).gameObject.SetActive(true);
            selectedSlot = null;
            selectedTower = null;
        }
    
        public void DeselectSkills() {
            selectedSkill = null;
            foreach (Skill skill in skillsList) {
                skill.Deselect();
            }
        }

        public void SelectSkill(Skill newSkill) {
            selectedSkill = newSkill;
        }

    }
}
