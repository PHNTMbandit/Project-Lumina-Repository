using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectLumina.Player.Input
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Project Lumina/Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameControls.IPlayerActions
    {
        #region Variables

        public GameControls GameControls { get; private set; }
        public bool AttackInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool InteractInput { get; private set; }
        public bool JumpInputPress { get; private set; }
        public bool JumpInputRelease { get; private set; }
        public Vector2 MoveInput { get; private set; }
        public bool RollInput { get; private set; }
        public bool SprintInput { get; private set; }

        public UnityAction onAttack, onDash, onInteract, onJump, onRoll, onSprint;

        #endregion Variables

        #region Unity Callback Functions

        private void OnEnable()
        {
            if (GameControls == null)
            {
                GameControls = new GameControls();
                GameControls.Player.AddCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        #endregion Unity Callback Functions

        #region Player Actions
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AttackInput = true;

                onAttack?.Invoke();
            }
            else if (context.canceled)
            {
                AttackInput = false;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DashInput = true;

                onDash?.Invoke();
            }
            else if (context.canceled)
            {
                DashInput = false;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InteractInput = true;

                onInteract?.Invoke();
            }
            else if (context.canceled)
            {
                InteractInput = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpInputPress = true;
                JumpInputRelease = false;

                onJump?.Invoke();
            }
            else if (context.canceled)
            {
                JumpInputPress = false;
                JumpInputRelease = true;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RollInput = true;

                onRoll?.Invoke();
            }
            else if (context.canceled)
            {
                RollInput = false;
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SprintInput = true;

                onSprint?.Invoke();
            }
            else if (context.canceled)
            {
                SprintInput = false;
            }
        }

        #endregion Player Actions

        #region Device Map Actions

        public void DisableGameplayInput()
        {
            GameControls.Player.Disable();
        }

        public void EnableGameplayInput()
        {
            GameControls.Player.Enable();
        }

        public void DisableAllInput()
        {
            GameControls.Player.Disable();
        }

        public void EnableAllInput()
        {
            GameControls.Player.Disable();
        }

        #endregion Device Map Actions
    }
}