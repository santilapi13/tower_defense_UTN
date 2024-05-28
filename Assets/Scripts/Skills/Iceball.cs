using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : ExplosionSkill {
    [SerializeField] private float freezeTime = 3f;
    protected override void AffectEnemy(Enemy enemy) {
        enemy.enemyMove.Freeze(freezeTime);
    }

    protected override void PlayFlyingSound() {
        AudioManager.Instance.PlaySFX("iceballFlying", false);
    }

    protected override void PlayExplosionSound() {
        AudioManager.Instance.PlaySFX("iceballExplosion", false);
    }
}
