using UnityEngine;

namespace Game.NPC.Harpy.States
{
    public class MovementState : HarpyState
    {
        private float _distanceToFinish = 1f;
        private float _speedMovement = 5f;
        private Vector3 _destination;
        private Rigidbody _rigidbody;
        
        public Vector3 Destination => _destination;
        
        public delegate void FinishMovement();
        public event FinishMovement OnFinishMovement;

        public MovementState(float speedMovement, Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _speedMovement = speedMovement;
        }
        
        public override void Process()
        {
            Move();
            
            if (IsFinish())
            {
                OnFinishMovement?.Invoke();
                OnFinishMovement = null;
            }
        }

        private void Move()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                Harpy.transform.position,
                _destination,
                _speedMovement * Time.deltaTime);

            Harpy.transform.position = newPosition;
            //_rigidbody.MovePosition(newPosition);
        }

        public void SetDestination(Vector3 value, float distanceToFinish)
        {
            _destination = value;
            _distanceToFinish = distanceToFinish;
            OnFinishMovement = null;
            OnFinishMovement += () => _destination = Harpy.transform.position;
        }

        private bool IsFinish()
        {
            return Vector3.Distance(StateMachine.transform.position, Destination) < _distanceToFinish;
        }
    }
}