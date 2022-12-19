using System;
using Game.NPC.Cyclope.States;
using NPC;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Game.NPC.Cyclope
{
    public class CyclopeStateMachine : MonoBehaviour
    {
        [Header("Common properties")]
        [SerializeField] private Cyclope cyclope;
        
        [Header("Attack properties")]
        [SerializeField] private Transform attackPoint;

        [Header("Fear properties")] 
        [SerializeField] private Transform[] areaPoints;
        
        [Header("Movement properties")] 
        [SerializeField] private float stopDistanceToPlayer = 4f;
        
        [Header("Gizmos")]
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private Color itemPathColor = Color.green;
        [SerializeField] private Color nearestItemColor = Color.cyan;
        
        public ShadowState ShadowState { get; private set; }
        public AttackState AttackState { get; private set; }
        
        public CyclopeState CurrentState { get; private set; }
        public CyclopeState DefaultState { get; private set; }
        
        
        public NavMeshAgent NavMeshAgent { get; private set; }

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();

            ShadowState = new ShadowState();
            AttackState = new AttackState(attackPoint, stopDistanceToPlayer);
            
            ShadowState.Initialize(this, cyclope);
            AttackState.Initialize(this, cyclope);
            
            SetState(AttackState);
        }

        private void Update()
        {
            CurrentState?.Process();
            DefaultState?.Process();
        }

        public void SetState(CyclopeState satyrState)
        {
            CurrentState?.Exit();
            CurrentState = satyrState;
            CurrentState.Enter();
        }

        public void SetDefaultState(CyclopeState satyrState)
        {
            DefaultState?.Exit();
            DefaultState = satyrState;
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