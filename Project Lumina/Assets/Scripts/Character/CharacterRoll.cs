using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Roll")]
    public class CharacterRoll : MonoBehaviour
    {
        public bool IsRolling { get; private set; }

        [ToggleGroup("Roll"), SerializeField]
        private bool Roll;

        [ToggleGroup("Roll"), Range(0, 10), SerializeField]
        private float _rollSpeed;

        [ToggleGroup("Roll"), SerializeField]
        private float _colliderSizeY, _colliderOffsetY;

        private float _defaultColliderOffsetY, _defaultColliderSizeY;
        private BoxCollider2D _collider;
        private CharacterMove _characterMove;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _characterMove = GetComponent<CharacterMove>();
            _rb = GetComponent<Rigidbody2D>();

            _defaultColliderSizeY = _collider.size.y;
            _defaultColliderOffsetY = _collider.offset.y;
        }

        public void RollCharacter()
        {
            IsRolling = true;

            _collider.size = new Vector2(_collider.size.x, _colliderSizeY);
            _collider.offset = new Vector2(_collider.offset.x, _colliderOffsetY);
            _rb.velocity = _characterMove.GetFacingDirection() * _rollSpeed;
        }

        public void FinishRoll()
        {
            IsRolling = false;

            _collider.size = new Vector2(_collider.size.x, _defaultColliderSizeY);
            _collider.offset = new Vector2(_collider.offset.x, _defaultColliderOffsetY);
        }
    }
}