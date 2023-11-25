using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Tower {
    
    
    protected override void Shoot(EnemyMove target) {
        GameObject bulletObject = Instantiate(bulletPrefab, bulletPoint.transform.position, Quaternion.identity);
        
        bulletObject.TryGetComponent(out Bullet bullet);
        
        bullet.target = target.transform;
        bullet.damage = damage;
        bullet.SetSpeed(bulletSpeed);
    }
}
