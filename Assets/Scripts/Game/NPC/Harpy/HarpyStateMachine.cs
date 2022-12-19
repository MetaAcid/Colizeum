using System;
using Game.NPC.Harpy.States;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


namespace Game.NPC.Harpy
{
    public class HarpyStateMachine : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentStateText;
        [SerializeField] private TextMeshProUGUI defaultStateText;
        
        [Header("Common properties")]
        [SerializeField] private Harpy harpy;
        
        [Header("Attack properties")]
        [SerializeField] private Transform attackPoint;

        [Header("Fear properties")] 
        [SerializeField] private Transform[] areaPoints;

        [Header("Movement properties")] 
        [SerializeField] private Transform[] safesPoints;
        [SerializeField] private float stopDistanceToPlayer = 4f;
        [SerializeField] private float speedMovement = 7f;
        
        [Header("Gizmos")]
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private Color itemPathColor = Color.green;
        [SerializeField] private Color nearestItemColor = Color.cyan;
        
        public MovementState ShadowState { get; private set; }
        public AttackState AttackState { get; private set; }
        public FearState FearState { get; private set; }
        public IdleState IdleState { get; private set; }
        
        
        public HarpyState CurrentState { get; private set; }
        public HarpyState DefaultState { get; private set; }
        

        private void Awake()
        {
            ShadowState = new MovementState(speedMovement, GetComponent<Rigidbody>());
            AttackState = new AttackState(attackPoint, stopDistanceToPlayer);
            FearState = new FearState(safesPoints);
            IdleState = new IdleState();
            
            ShadowState.Initialize(this, harpy);
            AttackState.Initialize(this, harpy);
            FearState.Initialize(this, harpy);
            IdleState.Initialize(this, harpy);
            
            SetState(AttackState);
            SetDefaultState(ShadowState);
        }

        private void Update()
        {
            currentStateText.text = CurrentState.ToString();
            defaultStateText.text = DefaultState.ToString();
            CurrentState?.Process();
        }

        private void FixedUpdate()
        {
            DefaultState?.Process();
        }

        public void SetState(HarpyState harpyState)
        {
            CurrentState?.Exit();
            CurrentState = harpyState;
            CurrentState.Enter();
        }

        public void SetDefaultState(HarpyState harpyState)
        {
            DefaultState?.Exit();
            DefaultState = harpyState;
            DefaultState.Enter();
        }

        public void SetDestination(Vector3 position, float distanceToFinish, Action onFinish)
        {
            DefaultState = ShadowState;
            ShadowState.SetDestination(position, distanceToFinish);
            ShadowState.OnFinishMovement += onFinish.Invoke;
        }

        private void OnDrawGizmos()
        {
            CurrentState?.DrawGizmos();
        }
    }
}