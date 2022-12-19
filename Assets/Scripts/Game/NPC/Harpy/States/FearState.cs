using Common;
using UnityEngine;

namespace Game.NPC.Harpy.States
{
    public class FearState : HarpyState
    {
        private Transform[] _safePoints;
        private Transform _currentPoint;

        public FearState(Transform[] safePoints)
        {
            _safePoints = safePoints;
        }

        public override void Enter()
        {
            base.Enter();
            _currentPoint = _safePoints[Random.Range(0, _safePoints.Length)];
        }

        public override void Process()
        {
            StateMachine.SetDestination(_currentPoint.position, 1, () =>
            {
                StateMachine.SetState(StateMachine.IdleState);
            });
            
            Harpy.transform.LookAt(_currentPoint);
        }
    }
}