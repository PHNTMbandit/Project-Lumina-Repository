using System.Collections;
using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
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
                damageable.Damage(_currentFallAttack.Damage);

                ObjectPoolController.Instance.GetPooledObject(_currentFallAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);
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