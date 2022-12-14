using System.Linq;
using Entities;
using Entities.Weapons;
using NPC;
using NPC.EnemyProperties;
using TMPro;
using UnityEngine;

namespace Game.NPC
{
    public class Enemy : MonoBehaviour, ITaker
    {
        [Header("Debug UI")] 
        [SerializeField] private TextMeshProUGUI healthText;
        
        [Header("Base properties")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;

        [Header("Arm properties")]
        [SerializeField] private ArmItemWeapon[] armItemWeapons;

        [Header("State machine")] 
        [SerializeField] private EnemyDefaultStateMachine stateMachine;
        
        [Space(20)]
        
        [SerializeField] private ArmItemWeapon leftHand;
        [SerializeField] private ArmItemWeapon rightHand;

        public ArmItemWeapon LeftHand => leftHand;
        public ArmItemWeapon RightHand => rightHand;
        
        public NpcProperty HealthProperty {get; private set;}
        public NpcProperty StaminaProperty {get; private set;}
        public NpcProperty DamageProperty { get; private set; }

        private void Awake()
        {
            HealthProperty = new NpcProperty(maxHealth);
            StaminaProperty = new NpcProperty(maxStamina);
            DamageProperty = new NpcProperty();

            HealthProperty.Value = 5;
            healthText.text = HealthProperty.MaxValue.ToString();
            HealthProperty.OnValueChange += () =>
            {
                healthText.text = HealthProperty.Value.ToString();
                if (HealthProperty.Value < HealthProperty.MaxValue)
                {
                    stateMachine.SetState(stateMachine.SearchingBoostState);
                }
            };
        }

        public void PickUp(ItemDataSO itemDataSO)
        {
            if (itemDataSO.ItemType == ItemType.Boostable)
            {
                HealthProperty.Value += ItemsManager.Instance.GetBoostItemDataSO(itemDataSO.ID).BoostAmount;
                return;
            }
            
            FindArmItemWeapon(itemDataSO.ID).Wear();
            rightHand = FindArmItemWeapon(itemDataSO.ID);
        }

        public ArmItemWeapon FindArmItemWeapon(string id)
        {
            return armItemWeapons.FirstOrDefault(item => item.WeaponDataSO.ID == id);
        }
    }
}