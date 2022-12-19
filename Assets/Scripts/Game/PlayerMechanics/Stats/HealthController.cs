using UnityEngine;
using UnityEngine.UI;

namespace Common.Stats
{
    public class HealthController : MonoBehaviour, IStatsUI
    {
        private const float MaxHealth = 1f;
        
        [SerializeField] private Image healthBar;
        
        [Header("Health Properties:")]
        [Space(10)]
        
        [Header("Health usage (in health bar percentage)")]
        [Range(0, 1)]
        [SerializeField] private float damageTaken;

        [Header("Health Recover")]
        [Range(0, 1)]
        [SerializeField] private float healthRegen;

        public void ReduceStat(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void RecoverStat()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateStatBar()
        {
            throw new System.NotImplementedException();
        }
    }
}