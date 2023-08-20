﻿using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Roll Attack State", menuName = "Project Lumina/States/Roll Attack State")]
    public class RollAttackState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.UseRollAttack();
            }
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (characterRollAttack.IsRollAttacking == false)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }
        }
    }
}