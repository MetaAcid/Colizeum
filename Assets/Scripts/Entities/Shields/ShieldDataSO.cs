using UnityEngine;

namespace Entities.Shields
{
    [CreateAssetMenu(fileName = "Shield", menuName = "ScriptableObjects/Shield", order = 1)]
    public class ShieldDataSO : ItemDataSO
    {
        [SerializeField] private float reduceDamagePercentage = 30;

        public float ReduceDamagePercentage => reduceDamagePercentage;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            itemType = ItemType.Shield;
        }
    }
}