using Entities;
using UnityEngine;

namespace Game.NPC.States.SearchingAreaStates
{
    public class SearchingWeaponState : SearchingBoostState
    {
        public override void Enter()
        {
            nearestItem = ItemsManager.Instance.FindNearestItem(StateMachine.transform.position, ItemType.Wearable);
        }

        public SearchingWeaponState(Color itemPathColor, Color nearestItemColor, float stopDistanceToItem) 
            : base(itemPathColor, nearestItemColor, stopDistanceToItem)
        {
            
        }
    }
}