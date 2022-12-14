using UnityEngine;

namespace Entities.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class WeaponDataSO : ItemDataSO
    {
        [SerializeField] private float damage = 5;
        [SerializeField] private float attackDelay = 1;
        [SerializeField] private float attackDuration = 2;
        [SerializeField] private float attackRange = 1.5f;

        public float Damage => damage;
        public float AttackDelay => attackDelay;
        public float AttackDuration => attackDuration;
        public float AttackRange => attackRange;

        protected override void OnValidate()
        {
            base.OnValidate();
            itemType = ItemType.Wearable;
        }
    }
}