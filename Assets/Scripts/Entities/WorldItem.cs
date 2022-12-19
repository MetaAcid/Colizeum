using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class WorldItem : MonoBehaviour
    {
        [SerializeField] private ItemDataSO itemDataSO;

        public ItemDataSO ItemDataSO => itemDataSO;
        public GameObject SpawnedItem => gameObject;
        
        protected virtual void Awake()
        {
            ItemsManager.Instance.AddWorldItem(this);
        }

        public void PickedUp(ITaker taker)
        {
            taker.PickUp(itemDataSO);
            ItemsManager.Instance.RemoveWorldItem(this);
            Destroy(SpawnedItem);
        }
    }
}