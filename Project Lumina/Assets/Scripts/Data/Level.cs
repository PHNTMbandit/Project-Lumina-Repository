using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Data
{
    [AddComponentMenu("Data/Level")]
    public class Level : MonoBehaviour
    {
        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value <= 0 ? 0 : value >= _maxLevel ? _maxLevel : value;
        }

        [BoxGroup("Level"), Range(1, 200), SerializeField]
        private int _maxLevel;

        [BoxGroup("Level"), ProgressBar(1, "_maxLevel"), SerializeField]
        private int _currentLevel = 1;

        [field: BoxGroup("XP"), ProgressBar(0, "RequiredXP"), ReadOnly, ShowInInspector]
        public int CurrentXP { get; private set; }

        [field: BoxGroup("XP"), ReadOnly, ShowInInspector]
        public int RequiredXP { get; private set; } = 0;

        [BoxGroup("XP"), SerializeField]
        private int _baseXP = 100;

        public UnityEvent onXPGain, onLevelUp;

        private void Awake()
        {
            CalculateRequiredXP();
        }

        private void CalculateRequiredXP()
        {
            RequiredXP = _baseXP * _currentLevel;
        }

        public void AddXP(int xpAmount)
        {
            CurrentXP += xpAmount;

            while (CurrentXP >= RequiredXP && _currentLevel < 99)
            {
                LevelUp();
            }

            onXPGain?.Invoke();
        }
        [Button]
        private void LevelUp()
        {
            _currentLevel++;
            CurrentXP -= RequiredXP;
            CalculateRequiredXP();

            onLevelUp?.Invoke();
        }
    }
}