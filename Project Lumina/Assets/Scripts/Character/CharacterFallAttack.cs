using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : CharacterAbility
    {
        public bool IsFallAttacking { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _comboIndex = 0;
        private bool _canContinueCombo;
        private Attack _currentFallAttack;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void FallAttack()
        {
            if (_canContinueCombo)
            {
                _comboIndex++;

                if (_comboIndex > _attackCombos.Length)
                {
                    _comboIndex = 1;
                }

                _currentFallAttack = _attackCombos[_comboIndex - 1];
                _animator.SetTrigger($"fall attack {_comboIndex}");

                _canContinueCombo = false;
                IsFallAttacking = true;
            }
        }

        public void FallAttackDamage()
        {
            foreach (Damageable damageable in _currentFallAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (damageable.IsDamageable)
                {
                    damageable.Damage(_currentFallAttack.Damage);

                    if (damageable.TryGetComponent(out HitFX hitFX))
                    {
                        hitFX.ShowHitFX(_currentFallAttack.HitFX.name);
                    }

                    if (damageable.TryGetComponent(out HitStop hitStop))
                    {
                        hitStop.Stop(_currentFallAttack.HitStopDuration);
                    }

                    if (damageable.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(_currentFallAttack.Damage, transform.position, _currentFallAttack.Colour);
                    }

                    if (damageable.TryGetComponent(out CameraShake cameraShake))
                    {
                        cameraShake.Shake(_currentFallAttack.Damage);
                    }
                }
            }
        }

        public void ContinueFallAttackCombo()
        {
            _canContinueCombo = true;
        }

        public void FinishFallAttack()
        {
            _comboIndex = 0;
            _canContinueCombo = true;
            IsFallAttacking = false;
        }
    }
}