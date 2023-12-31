using System;
using ProjectLumina.Capabilities;
using ProjectLumina.Character;
using ProjectLumina.Data;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterFall))]
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Health))]
    public abstract class CharacterStateController : StateController
    {
        public Animator Animator { get; private set; }
        public CharacterFall CharacterFall { get; private set; }
        public CharacterMove CharacterMove { get; private set; }
        public Damageable Damageable { get; private set; }
        public Health Health { get; private set; }

        protected CharacterAbility[] abilities;

        protected override void Awake()
        {
            base.Awake();

            abilities = GetComponents<CharacterAbility>();
            Animator = GetComponent<Animator>();
            CharacterFall = GetComponent<CharacterFall>();
            CharacterMove = GetComponent<CharacterMove>();
            Damageable = GetComponent<Damageable>();
            Health = GetComponent<Health>();
        }

        public bool HasCharacterAbility<T>(out T characterAbility) where T : CharacterAbility
        {
            T ability = (T)Array.Find(abilities, i => i.GetType() == typeof(T));
            characterAbility = ability;

            if (ability != null)
            {
                if (ability.IsUnlocked)
                {
                    return true;
                }
            }

            return false;
        }
    }
}