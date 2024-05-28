using System.Collections;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour {
    private bool isSelected;
    private float cooldown;
    [SerializeField] private float cooldownTime = 10f;
    [SerializeField] private Image skillImage;
    [SerializeField] private Image skillIcon;
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private GameObject spawnPoint;
    private Color defaultColor;
    
    void Start() {
        skillIcon.fillAmount = 1;
        isSelected = false;
        cooldown = 0;
        defaultColor = skillImage.color;
    }
    
    void Update() {
        if (GameManager.Instance.IsCurrentWaveActive())
            HandleCooldown();
    }

    private void HandleCooldown() {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
            skillIcon.fillAmount = 1f - (cooldown / cooldownTime);
        }
        else {
            skillIcon.fillAmount = 1;
        }
    }

    public void Deselect() {
        isSelected = false;
        skillImage.color = defaultColor;
        HUDManager.Instance.SelectSkill(null);
    }
    
    public void OnClick() {
        if (cooldown > 0 || !GameManager.Instance.IsCurrentWaveActive()) {
            return;
        }
        
        if (!isSelected) {
            HUDManager.Instance.DeselectSkills();
            skillImage.color = Color.green;
            isSelected = true;
            HUDManager.Instance.SelectSkill(this);
        }
        else {
            Deselect();
        }
    }

    public void PerformSkill(Vector3 point) {
        GameObject skillInstance = Instantiate(skillPrefab);
        Vector3 spawnPosition = spawnPoint.transform.position;
        skillInstance.transform.position = new Vector3(point.x + 5, spawnPosition.y, spawnPosition.z);
        cooldown = cooldownTime;
        ExplosionSkill skill = skillInstance.GetComponent<ExplosionSkill>();
        skill.ActiveMovement(new Vector3(point.x, 1f, point.z));
        Deselect();
    } 
}
