using System;
using System.Linq;
using Entities;
using Entities.Shields;
using Entities.Weapons;
using Game.Player;
using Game.User;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game.PlayerMechanics
{
    public class Player : MonoBehaviour, ITaker
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
        [SerializeField] private ArmItemShield[] armItemShields;
        [SerializeField] private KeyCode takeButton;

        [Space(20)]
        
        private ArmItemShield _leftHand;
        private ArmItemWeapon _rightHand;
        
        public bool IsBlocking { get; set; }

        public ArmItemShield LeftHand => _leftHand;
        public ArmItemWeapon RightHand => _rightHand;

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

            HealthProperty.OnZeroValue += () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            };

            healthPropertyUI.PlayerProperty = HealthProperty;
            staminaPropertyUI.PlayerProperty = StaminaProperty;
        }

        private void Update()
        {
            if (Input.GetKeyDown(takeButton))
            {
                TryPickUp();
            }
        }

        private void TryPickUp()
        {
            Ray R = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height /2));
            RaycastHit hit;
            if (Physics.Raycast(R, out hit, 2f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.TryGetComponent<WorldItem>(out var item))
                {
                    item.PickedUp(this);
                }
            }
        }

        public void PickUp(ItemDataSO itemDataSO)
        {
            Debug.Log("Player picked up item. ID: " + itemDataSO.ID);
            if (itemDataSO.ItemType == ItemType.Boostable)
            {
                HealthProperty.Value += ItemsManager.Instance.GetBoostItemDataSO(itemDataSO.ID).BoostAmount;
                return;
            }

            
            if (itemDataSO.ItemType == ItemType.Shield)
            {
                _leftHand = FindArmItemShield(itemDataSO.ID);
                _leftHand.Wear();
                return;
                
            }
            ArmItemWeapon rightHand = FindArmItemWeapon(itemDataSO.ID);
            rightHand.Wear();
            _rightHand = rightHand;
            DamageProperty.Value = RightHand.WeaponDataSO.Damage;
        }

        public ArmItemWeapon FindArmItemWeapon(string id)
        {
            return armItemWeapons.FirstOrDefault(item => item.WeaponDataSO.ID == id);
        }

        public ArmItemShield FindArmItemShield(string id)
        {
            return armItemShields.FirstOrDefault(item => item.ShieldDataSO.ID == id);
        }
    }
}