using System;
using ProjectLumina.Capabilities;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data
{
    [Serializable, HideLabel]
    public class RangedAttack : Attack
    {
        public override bool TryAttack(GameObject user, Damageable target)
        {
            if (target.IsDamageable)
            {
                Damage.SetBaseValue(damage);
                CriticalChance.SetBaseValue(criticalChance);
                CriticalDamageMultiplier.SetBaseValue(criticalDamageMultiplier);

                if (UnityEngine.Random.Range(0, 100) <= criticalChance)
                {
                    float criticalDamage = Damage.Value * (criticalDamageMultiplier / 100) + damage;
                    target.Damage(criticalDamage);

                    if (target.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(true, criticalDamage, user.transform.position, criticalDamageIndicatorColour);
                    }
                }
                else
                {
                    target.Damage(Damage.Value);

                    if (target.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(false, Damage.Value, user.transform.position, damageIndicatorColour);
                    }
                }

                if (target.TryGetComponent(out HitFX hitFX))
                {
                    hitFX.ShowHitFX(base.hitFX.name);
                }

                if (target.TryGetComponent(out HitStop hitStop))
                {
                    hitStop.Stop(hitStopDuration);
                }

                if (target.TryGetComponent(out CameraShake cameraShake))
                {
                    cameraShake.Shake(Damage.Value);
                }

                return true;
            }

            return false;
        }
    }
}