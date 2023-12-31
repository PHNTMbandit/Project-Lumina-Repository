using ProjectLumina.Character;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    [CreateAssetMenu(fileName = "New Critical Chance Component", menuName = "Project Lumina/Neuroglyphs/Components/Critical Chance", order = 0)]
    public class CriticalChanceComponent : NeuroglyphComponent
    {
        [Range(0, 1000), SuffixLabel("%"), SerializeField]
        private float _criticalChanceModifier;

        public override void Activate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterAerialAttack aerialAttack))
            {
                foreach (var attack in aerialAttack.GetAerialAttacks())
                {
                    attack.CriticalChance.AddModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterFallAttack fallAttack))
            {
                foreach (var attack in fallAttack.GetFallAttacks())
                {
                    attack.CriticalChance.AddModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterMeleeAttack meleeAttack))
            {
                foreach (var attack in meleeAttack.GetMeleeAttacks())
                {
                    attack.CriticalChance.AddModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterRollAttack rollAttack))
            {
                foreach (var attack in rollAttack.GetRollAttacks())
                {
                    attack.CriticalChance.AddModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterShoot shoot))
            {
                foreach (var attack in shoot.GetRangedAttacks())
                {
                    attack.CriticalChance.AddModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }
        }

        public override void Deactivate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterAerialAttack aerialAttack))
            {
                foreach (var attack in aerialAttack.GetAerialAttacks())
                {
                    attack.CriticalChance.RemoveModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterFallAttack fallAttack))
            {
                foreach (var attack in fallAttack.GetFallAttacks())
                {
                    attack.CriticalChance.RemoveModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterMeleeAttack meleeAttack))
            {
                foreach (var attack in meleeAttack.GetMeleeAttacks())
                {
                    attack.CriticalChance.RemoveModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterRollAttack rollAttack))
            {
                foreach (var attack in rollAttack.GetRollAttacks())
                {
                    attack.CriticalChance.RemoveModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }

            if (user.TryGetComponent(out CharacterShoot shoot))
            {
                foreach (var attack in shoot.GetRangedAttacks())
                {
                    attack.CriticalChance.RemoveModifier(new PercentageStatModifier(_criticalChanceModifier));
                }
            }
        }

        public override string GetComponentDescription()
        {
            return $"Critical chance is increased by {_criticalChanceModifier}%";
        }
    }
}