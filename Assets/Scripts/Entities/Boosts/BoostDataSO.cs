using UnityEngine;

namespace Entities.Boosts
{
    [CreateAssetMenu(fileName = "Boost", menuName = "ScriptableObjects/Boost", order = 1)]
    public class BoostDataSO : ItemDataSO
    {
        [SerializeField] private float boostAmount;

        public float BoostAmount => boostAmount;

        protected override void OnValidate()
        {
            base.OnValidate();
            itemType = ItemType.Boostable;
        }
    }
}