using UnityEngine;

namespace Game.NPC.Cyclope.States
{
    public class ShadowState : CyclopeState
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
            OnFinishMovement += () => _destination = Cyclope.transform.position;
        }

        private bool IsFinish()
        {
            return Vector3.Distance(StateMachine.transform.position, Destination) < _distanceToFinish;
        }
    }
}