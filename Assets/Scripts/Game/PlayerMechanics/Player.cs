using System.Linq;
using Entities;
using Entities.Weapons;
using Game.Player;
using Game.User;
using UnityEngine;

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
        [SerializeField] private KeyCode takeButton;

        [Space(20)]
        
        private ArmItemWeapon _leftHand;
        private ArmItemWeapon _rightHand;

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
            if (Input.GetKeyDown(takeButton))
            {
                Ray R = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height /2));
                RaycastHit hit;
                if (Physics.Raycast(R, out hit, 2f))
                {
                    if (itemDataSO.ItemType == ItemType.Boostable)
                    {
                        HealthProperty.Value += ItemsManager.Instance.GetBoostItemDataSO(itemDataSO.ID).BoostAmount;
                        return;
                    }
            
                    FindArmItemWeapon(itemDataSO.ID).Wear();
                    _rightHand = FindArmItemWeapon(itemDataSO.ID);
                    DamageProperty.Value = _rightHand.WeaponDataSO.Damage;
                }
            }
        }

        public ArmItemWeapon FindArmItemWeapon(string id)
        {
            return armItemWeapons.FirstOrDefault(item => item.WeaponDataSO.ID == id);
        }
    }
}