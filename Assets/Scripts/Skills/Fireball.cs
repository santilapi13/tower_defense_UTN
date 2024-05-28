using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ExplosionSkill {
    [SerializeField] protected string explosionAudio;
    [SerializeField] protected string flyingAudio;

    protected void Start() {
        flyingAudio = "fireballFlying";
        explosionAudio = "fireballExplosion";
    }
    protected override void AffectEnemy(Enemy enemy) {
        enemy.TakeDamage(damage);
    }

    protected override void PlayFlyingSound() {
        AudioManager.Instance.PlaySFX("fireballFlying", false);
    }

    protected override void PlayExplosionSound() {
        AudioManager.Instance.PlaySFX("fireballExplosion", false);
    }
}
