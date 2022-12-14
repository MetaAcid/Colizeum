using Entities;
using NPC;
using UnityEngine;

namespace Game.NPC.States.SearchingAreaStates
{
    public class SearchingAreaState : EnemyState
    {
        private readonly float _stopDistanceToItem;
        
        private readonly Color _itemPathColor;
        private readonly Color _nearestItemColor;

        protected WorldItem nearestItem;

        public SearchingAreaState(Color itemPathColor, Color nearestItemColor, float stopDistanceToItem)
        {
            _itemPathColor = itemPathColor;
            _nearestItemColor = nearestItemColor;
            _stopDistanceToItem = stopDistanceToItem;
        }

        public override void Process()
        {
            if (!nearestItem) return;
            StateMachine.SetDestination(nearestItem.SpawnedItem.transform.position, _stopDistanceToItem,() =>
            {
                nearestItem.PickedUp(Enemy);
                StateMachine.SetState(StateMachine.AttackState);
            });
        }
        
        public override void DrawGizmos()
        {
            if (!ItemsManager.Instance) return;
            if (!nearestItem) return;
            
            Gizmos.color = _itemPathColor;
            foreach (var item in ItemsManager.Instance.WorldItems)
            {
                Gizmos.DrawLine(StateMachine.transform.position, item.SpawnedItem.transform.position);
            }

            Gizmos.color = _nearestItemColor;
            Gizmos.DrawLine(StateMachine.transform.position, nearestItem.SpawnedItem.transform.position);
        }
    }
}