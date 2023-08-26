using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using ProjectLumina.Player.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Interactor")]
    public class CharacterInteractor : CharacterAbility
    {
        [FoldoutGroup("References"), SerializeField]
        private InputReader _inputReader;

        [FoldoutGroup("References"), SerializeField]
        private RangeSensor2D _sensor;

        public UnityEvent<Interactable> onInteractableDetected;
        public UnityEvent onInteractableLost, onInteract;

        private void Awake()
        {
            _inputReader.onInteract += OnInteract;

            _sensor.OnDetected.AddListener(OnInteractableDetected);
            _sensor.OnLostDetection.AddListener(OnInteractableLost);
        }

        public void OnInteract()
        {
            if (IsUnlocked)
            {
                if (_sensor.GetNearestDetection() != null)
                {
                    _sensor.GetNearestComponent<Interactable>().Interact();
                }
            }
        }

        public void OnInteractableDetected(GameObject gameObject, Sensor sensor)
        {
            if (IsUnlocked)
            {
                if (gameObject.TryGetComponent(out Interactable interactable))
                {
                    onInteractableDetected?.Invoke(interactable);
                }
            }
        }

        public void OnInteractableLost(GameObject gameObject, Sensor sensor)
        {
            if (IsUnlocked)
            {
                onInteractableLost?.Invoke();
            }
        }
    }
}