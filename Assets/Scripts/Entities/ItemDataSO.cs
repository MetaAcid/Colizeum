using UnityEngine;

namespace Entities
{
    public enum ItemType
    {
        Weapon,
        Boostable
    }
    
    public class ItemDataSO : ScriptableObject
    {
        [SerializeField] private string id = "ENTER ID";
        [SerializeField] private string itemName = "ENTER NAME";
        [SerializeField] private GameObject worldModel;
        
        protected ItemType itemType;

        public string ID => id;
        public GameObject WorldModel => worldModel;
        public ItemType ItemType => itemType;
        
        protected virtual void OnValidate()
        {
            if (id.Length == 0) id = "ENTER ID";
            if (itemName.Length == 0) itemName = "ENTER NAME";
        }
    }
}