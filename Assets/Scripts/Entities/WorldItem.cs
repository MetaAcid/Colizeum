using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class WorldItem : MonoBehaviour
    {
        [SerializeField] private ItemDataSO itemDataSO;

        public ItemDataSO ItemDataSO => itemDataSO;
        public GameObject SpawnedItem { get; protected set; }
        
        protected virtual void Awake()
        {
            SpawnItem();
        }

        protected void SpawnItem()
        {
            SpawnedItem = Instantiate(
                itemDataSO.WorldModel, 
                transform.position,
                itemDataSO.WorldModel.transform.rotation);
            
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