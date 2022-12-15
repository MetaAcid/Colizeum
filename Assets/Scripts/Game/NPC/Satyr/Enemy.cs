using System;
using System.Linq;
using Common;
using Entities;
using Entities.Weapons;
using NPC;
using NPC.EnemyProperties;
using NPC.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.NPC
{
    public enum EnemyGeneralState
    {
        Start,
        Attack,
        Fear
    }
    
    public class Enemy : MonoBehaviour, ITaker
    {
        public static Enemy Instance { get; private set; }
        
        [Header("Debug UI")] 
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI damageText;
        
        [Header("Base properties")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        [SerializeField] private float safeDistanceToPlayer = 5;

        [Header("Arm properties")]
        [SerializeField] private ArmItemWeapon[] armItemWeapons;

        [Header("State machine")] 
        [SerializeField] private EnemyDefaultStateMachine stateMachine;
        
        private ArmItemWeapon _leftHand;
        private ArmItemWeapon _rightHand;
        private EnemyGeneralState _enemyGeneralState;

        public ArmItemWeapon LeftHand => _leftHand;
        public ArmItemWeapon RightHand => _rightHand;
        
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
                healthText.text = HealthProperty.Value.ToString();
                if (IsCriticalValue(HealthProperty))
                {
                    stateMachine.SetState(stateMachine.FearState);
                    return;
                }
                
                stateMachine.SetState(stateMachine.AttackState);
            };

            HealthProperty.OnZeroValue += Die;

            DamageProperty.OnValueChange += () =>
            {
                damageText.text = DamageProperty.Value.ToString();
            };
        }

        private void Update()
        {
            transform.LookAt(PlayerMechanics.Player.Instance.transform);
        }

        public void PickUp(ItemDataSO itemDataSO)
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

        public ArmItemWeapon FindArmItemWeapon(string id)
        {
            return armItemWeapons.FirstOrDefault(item => item.WeaponDataSO.ID == id);
        }

        private bool IsCriticalValue(NpcProperty npcProperty)
        {
            return Utils.GetPercentage(npcProperty.Value, npcProperty.MaxValue) <= GameConfig.CriticalEnemyValuePercentage;
        }

        public bool IsSaveDistance()
        {
            return Vector3.Distance(PlayerMechanics.Player.Instance.transform.position, transform.position) >=
                   safeDistanceToPlayer;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}