using System.Linq;
using Entities;
using Entities.Weapons;
using Game.User;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        
        [Header("Base properties")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;

        [Header("UI")] 
        [SerializeField] private PlayerPropertyUI healthPropertyUI;
        [SerializeField] private PlayerPropertyUI staminaPropertyUI;
        
        [Header("Arm properties")]
        [SerializeField] private ArmItemWeapon[] armItemWeapons;

        [Space(20)]
        
        [SerializeField] private ArmItemWeapon leftHand;
        [SerializeField] private ArmItemWeapon rightHand;

        public ArmItemWeapon LeftHand => leftHand;
        public ArmItemWeapon RightHand => rightHand;

        public PlayerProperty HealthProperty { get; private set; }
        public PlayerProperty StaminaProperty { get; private set; }
        public PlayerProperty DamageProperty { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            HealthProperty = new PlayerProperty(maxHealth);
            StaminaProperty = new PlayerProperty(maxStamina);
            DamageProperty = new PlayerProperty();

            healthPropertyUI.PlayerProperty = HealthProperty;
            staminaPropertyUI.PlayerProperty = StaminaProperty;
        }
        
        public void PickUp(ItemDataSO itemDataSO)
        {
            FindArmItemWeapon(itemDataSO.ID).Wear();
            rightHand = FindArmItemWeapon(itemDataSO.ID);
        }

        public ArmItemWeapon FindArmItemWeapon(string id)
        {
            return armItemWeapons.FirstOrDefault(item => item.WeaponDataSO.ID == id);
        }
    }
}