using Entities.Weapons;
using NPC.EnemyProperties;
using UnityEngine;

namespace Game.NPC.Harpy
{
    public class Harpy : MonoBehaviour
    {
        [Header("Base properties")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        
        [SerializeField] private ArmItemWeapon rightHand;

        [SerializeField] private HarpyStateMachine harpyStateMachine;

        public ArmItemWeapon RightHand => rightHand;
        
        public NpcProperty HealthProperty {get; private set;}
        public NpcProperty StaminaProperty {get; private set;}
        public NpcProperty DamageProperty { get; private set; }
        
        private void Awake()
        {
            HealthProperty = new NpcProperty(maxHealth);
            StaminaProperty = new NpcProperty(maxStamina);
            DamageProperty = new NpcProperty();

            HealthProperty.OnValueChange += () =>
            {
                Debug.Log("Harpy took damage!");
                harpyStateMachine.SetState(harpyStateMachine.FearState);
            };
            
            HealthProperty.OnZeroValue += () =>
            {
                GameManager.Instance.EnemyDie();
                Die();
            };
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}