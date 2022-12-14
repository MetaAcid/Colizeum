using Game.User;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Stats
{
    public class StaminaController : MonoBehaviour, IStatsUI
    {
        const float MaxStamina = 1f;
        
        [SerializeField] private Image staminaBar;
        
        [Header("Stamina Properties:")]
        [Space(10)]
        
        [Header("Stamina usage (in stamina bar percentage)")]
        [Range(0, 1)]
        [SerializeField] private float jumpCost;
        [Range(0, 1)]
        [SerializeField] private float runPerSecondCost;
        
        [Header("Stamina Recover")]
        [SerializeField] private float secondsUntilRegen;
        [Range(0, 1)]
        [SerializeField] private float regenPerSecond;
        
        private MovementManager _movementManager;

        private float _currentStamina;
        private float _staminaNotUsedTimer = 0f;
        
        void Awake()
        {
            _movementManager = transform.parent.gameObject.GetComponent<MovementManager>();
            _currentStamina = MaxStamina;
            UpdateStatBar();
        }

        private void OnEnable()
        {
            _movementManager.onPlayerJump += UseStaminaToJump;
            _movementManager.onPlayerRun += UseStaminaToRun;
        }

        private void OnDisable()
        {
            _movementManager.onPlayerJump -= UseStaminaToJump;
            _movementManager.onPlayerRun -= UseStaminaToRun;
        }

        void Update()
        {
            _staminaNotUsedTimer += Time.deltaTime;
            if (StaminaShouldRegen())
            {
                RecoverStat();
            }
        }
        public void ReduceStat(float amount)
        {
            if (amount <= 0)
            {
                return;
            }

            _staminaNotUsedTimer = 0;

            if (_currentStamina - amount >= 0)
            {
                _currentStamina -= amount;
                UpdateStatBar();
            }
        }

        public void RecoverStat()
        {
            float regenAmount = regenPerSecond * Time.deltaTime;
            if (_currentStamina + regenAmount <= MaxStamina)
            {
                _currentStamina += regenAmount;
                UpdateStatBar();
            }
        }

        public void UpdateStatBar()
        {
            staminaBar.fillAmount = _currentStamina;
        }
        
        private bool StaminaShouldRegen()
        {
            if (_staminaNotUsedTimer <= secondsUntilRegen)
            {
                return false;
            }
            return true;
        }

        private void UseStaminaToJump()
        {
            ReduceStat(jumpCost);
        }

        private void UseStaminaToRun()
        {
            float runPerFrameCost = runPerSecondCost * Time.deltaTime;
            ReduceStat(runPerFrameCost);
        }
    }
}