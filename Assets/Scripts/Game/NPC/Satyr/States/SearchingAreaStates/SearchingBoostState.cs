using Entities;
using UnityEngine;

namespace Game.NPC.States.SearchingAreaStates
{
    public class SearchingBoostState : SearchingAreaState
    {
        public override void Enter()
        {
            nearestItem = ItemsManager.Instance.FindNearestItem(StateMachine.transform.position, ItemType.Boostable);

            if (nearestItem) return;
            
            StateMachine.SetState(StateMachine.AttackState);
        }

        public SearchingBoostState(Color itemPathColor, Color nearestItemColor, float stopDistanceToItem) 
            : base(itemPathColor, nearestItemColor, stopDistanceToItem)
        {
            OnProcess += () =>
            {
                if (!Enemy.IsSaveDistance())
                {
                    StateMachine.SetState(StateMachine.FearState);
                }
            };
        }
    }
}