using System;
using System.Collections.Generic;
using Entities.Boosts;
using Entities.Weapons;
using NPC.States;
using UnityEngine;

namespace Entities
{
    public struct ItemDestination
    {
        public float Distance { get; set; }
        public WorldItem WorldItemWeapon { get; set; }

        public ItemDestination(float distance, WorldItem worldItemWeapon)
        {
            Distance = distance;
            WorldItemWeapon = worldItemWeapon;
        }
    }
    
    public class ItemsManager : MonoBehaviour
    {
        public static ItemsManager Instance;
        [SerializeField] private BoostDataSO[] allBoostItems;

        private List<WorldItem> _worldItemWeapons = new List<WorldItem>();
        public WorldItem[] WorldItems => _worldItemWeapons.ToArray();
        public BoostDataSO[] AllBoostItems => allBoostItems;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void AddWorldItem(WorldItem worldItem)
        {
            _worldItemWeapons.Add(worldItem);
        }

        public void RemoveWorldItem(WorldItem worldItem)
        {
            Destroy(Array.Find(_worldItemWeapons.ToArray(), item => item == worldItem));
            _worldItemWeapons.Remove(worldItem);
        }

        public BoostDataSO GetBoostItemDataSO(string id)
        {
            foreach (var item in allBoostItems)
            {
                if (item.ID == id) return item;
            }

            throw new Exception($"Item with id {id} did not find!");
        }
        
        public WorldItem FindNearestItem(Vector3 selfPosition, ItemType itemType)
        {
            ItemDestination itemDestination = new ItemDestination(float.MaxValue, null);
            Debug.Log(WorldItems.Length);
            foreach (var item in WorldItems)
            {
                float itemDistance = Vector3.Distance(selfPosition, item.SpawnedItem.transform.position);
                if (itemDestination.Distance < itemDistance || item.ItemDataSO.ItemType != itemType) continue;
                
                itemDestination.Distance = itemDistance;
                itemDestination.WorldItemWeapon = item;
            }
            return itemDestination.WorldItemWeapon;
        }
    }
}