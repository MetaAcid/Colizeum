using Entities.Weapons;
using NPC.EnemyProperties;
using UnityEngine;

namespace Game.NPC.Cyclope
{
    public class Cyclope : MonoBehaviour
    {
        [Header("Base properties")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        
        [SerializeField] private ArmItemWeapon rightHand;

        public ArmItemWeapon RightHand => rightHand;
        
        public NpcProperty HealthProperty {get; private set;}
        public NpcProperty StaminaProperty {get; private set;}
        public NpcProperty DamageProperty { get; private set; }
        
        private void Awake()
        {
            HealthProperty = new NpcProperty(maxHealth);
            StaminaProperty = new NpcProperty(maxStamina);
            DamageProperty = new NpcProperty();

            HealthProperty.OnZeroValue += () =>
            {
                GameManager.Instance.EnemyDie();
                Die();
            };

            DamageProperty.OnValueChange += () =>
            {
            };
        }
        
        private void Update()
        {
            transform.LookAt(PlayerMechanics.Player.Instance.transform);
        }
        
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}