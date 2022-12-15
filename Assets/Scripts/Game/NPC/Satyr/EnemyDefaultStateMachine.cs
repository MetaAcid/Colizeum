using System;
using System.Data.Common;
using Game.NPC;
using Game.NPC.States;
using Game.NPC.States.SearchingAreaStates;
using NPC.States;
using NPC.States.DefaultState;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class EnemyDefaultStateMachine : MonoBehaviour
    {
        [Header("Debug UI")] 
        [SerializeField] private TextMeshProUGUI currentStateText;
        [SerializeField] private TextMeshProUGUI movementStateText;
        
        [Header("Common properties")]
        [SerializeField] private Enemy enemy;
        
        [Header("Attack properties")]
        [SerializeField] private Transform attackPoint;

        [Header("Fear properties")] 
        [SerializeField] private Transform[] areaPoints;
        
        [Header("Movement properties")] 
        [SerializeField] private float stopDistanceToEntity = 1f;
        [SerializeField] private float stopDistanceToPlayer = 4f;
        
        [Header("Gizmos")]
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private Color itemPathColor = Color.green;
        [SerializeField] private Color nearestItemColor = Color.cyan;
        
        public IdleState IdleState { get; private set; }
        public MovementState MovementState { get; private set; }
        public SearchingWeaponState SearchingWeaponState { get; private set; }
        public SearchingBoostState SearchingBoostState { get; private set; }
        public AttackState AttackState { get; private set; }
        public FearState FearState { get; private set; }
        
        public EnemyState CurrentState { get; private set; }
        public EnemyState DefaultState { get; private set; }
        
        
        public NavMeshAgent NavMeshAgent { get; private set; }

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();

            IdleState = new IdleState();
            MovementState = new MovementState();
            SearchingWeaponState = new SearchingWeaponState(itemPathColor, nearestItemColor, stopDistanceToEntity);
            SearchingBoostState = new SearchingBoostState(itemPathColor, nearestItemColor, stopDistanceToEntity);
            AttackState = new AttackState(attackPoint, stopDistanceToPlayer);
            FearState = new FearState(areaPoints);
            
            IdleState.Initialize(this, enemy);
            MovementState.Initialize(this, enemy);
            SearchingWeaponState.Initialize(this, enemy);
            SearchingBoostState.Initialize(this, enemy);
            AttackState.Initialize(this, enemy);
            FearState.Initialize(this, enemy);
            
            SetState(SearchingWeaponState);
        }

        private void Update()
        {
            CurrentState?.Process();
            DefaultState?.Process();

            movementStateText.text = DefaultState?.ToString();
        }

        public void SetState(EnemyState enemyState)
        {
            CurrentState?.Exit();
            CurrentState = enemyState;
            currentStateText.text = CurrentState?.ToString();
            CurrentState.Enter();
        }

        public void SetDefaultState(EnemyState enemyState)
        {
            DefaultState?.Exit();
            DefaultState = enemyState;
            movementStateText.text = DefaultState?.ToString();
            DefaultState.Enter();
        }

        public void SetDestination(Vector3 position, float distanceToFinish, Action onFinish)
        {
            DefaultState = MovementState;
            MovementState.SetDestination(position, distanceToFinish);
            MovementState.OnFinishMovement += onFinish.Invoke;
        }

        private void OnDrawGizmos()
        {
            CurrentState?.DrawGizmos();
        }
    }
}
