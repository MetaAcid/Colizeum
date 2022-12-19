using Game.Player;
using UnityEngine;

namespace NPC.States.DefaultState
{
    public class MovementState : SatyrState
    {
        private float _distanceToFinish = 1f;
        private Vector3 _destination;
        
        public Vector3 Destination => _destination;
        
        public delegate void FinishMovement();
        public event FinishMovement OnFinishMovement;
        
        public override void Process()
        {
            StateMachine.NavMeshAgent.SetDestination(_destination);

            if (IsFinish())
            {
                OnFinishMovement?.Invoke();
                OnFinishMovement = null;
            }
        }

        public void SetDestination(Vector3 value, float distanceToFinish)
        {
            _destination = value;
            _distanceToFinish = distanceToFinish;
            OnFinishMovement = null;
            OnFinishMovement += () => _destination = Satyr.transform.position;
        }

        private bool IsFinish()
        {
            return Vector3.Distance(StateMachine.transform.position, Destination) < _distanceToFinish;
        }
    }
}