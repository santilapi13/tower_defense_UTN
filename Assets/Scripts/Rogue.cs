using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rogue : Tower {
    [SerializeField] private Animator anim;
    
    protected override void Shoot(EnemyMove target) {
        anim.SetTrigger("Shoot");
        AudioManager.Instance.PlaySFX("bowShot", false);
        GameObject bulletObject = Instantiate(bulletPrefab, bulletPoint.transform.position, Quaternion.identity);
        
        bulletObject.TryGetComponent(out Bullet bullet);
        
        bullet.target = target.transform;
        bullet.damage = damage;
        bullet.SetSpeed(bulletSpeed);
    }
}
