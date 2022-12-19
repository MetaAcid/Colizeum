using UnityEngine;

namespace Game.PlayerMechanics.Stats
{
    public class StaminaController : MonoBehaviour
    {
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
        
        private PlayerMovement _playerMovement;

        private float _staminaNotUsedTimer = 0f;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            _playerMovement.onPlayerJump += UseStaminaToJump;
            _playerMovement.onPlayerRun += UseStaminaToRun;
        }

        private void Update()
        {
            _staminaNotUsedTimer += Time.deltaTime;
            if (StaminaShouldRegen())
            {
                RecoverStat();
            }
        }
        public void ReduceStat(float amount)
        {
            _staminaNotUsedTimer = 0;
            Player.Instance.StaminaProperty.Value -= amount;
            Debug.Log(Player.Instance.StaminaProperty.Value);
        }

        public void RecoverStat()
        {
            float regenAmount = regenPerSecond * Time.deltaTime;
            if (Player.Instance.StaminaProperty.Value + regenAmount <= Player.Instance.StaminaProperty.MaxValue)
            {
                Player.Instance.StaminaProperty.Value += regenAmount;

            }
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