using ProjectLumina.Character;
using ProjectLumina.Data.StatusEffects;
using ProjectLumina.Factories;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    [CreateAssetMenu(fileName = "New Status Effect Component", menuName = "Project Lumina/Neuroglyphs/Components/Status Effect", order = 0)]
    public class StatusEffectComponent : NeuroglyphComponent
    {
        [SerializeField]
        private StatusEffect _statusEffect;

        [Range(0, 100), SuffixLabel("%"), SerializeField]
        private float _afflictionChance;

        private readonly StatusEffectFactory _statusEffectFactory = new();

        public override void Activate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterMeleeAttack meleeAttack))
            {
                meleeAttack.onHit += TryApplyStatusEffect;
            }
        }

        public override void Deactivate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterMeleeAttack meleeAttack))
            {
                meleeAttack.onHit -= TryApplyStatusEffect;
            }
        }

        private void TryApplyStatusEffect(GameObject target)
        {
            if (target.TryGetComponent(out CharacterStatusEffects characterStatusEffects))
            {
                if (Random.Range(0, 100) <= _afflictionChance)
                {
                    characterStatusEffects.AddStatusEffect(_statusEffectFactory.GetStatusEffect(_statusEffect));
                }
            }
        }

        public override string GetComponentDescription()
        {
            return $"Your attacks have {_afflictionChance}% chance of afflicting {_statusEffect.StatusEffectName}";
        }
    }
}