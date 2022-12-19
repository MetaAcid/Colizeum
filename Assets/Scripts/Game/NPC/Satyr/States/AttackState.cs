using System.Collections;
using Common;
using Game.Player;
using Game.Player.Legacy;
using Game.PlayerMechanics;
using Game.User;
using UnityEngine;

namespace NPC.States
{
    public class AttackState : SatyrState
    {
        private readonly float _stopDistanceToPlayer;
        private readonly Transform _attackPoint;
        private bool _isAttacking = false;

        public AttackState(Transform attackPoint, float stopDistanceToPlayer)
        {
            _attackPoint = attackPoint;
            _stopDistanceToPlayer = stopDistanceToPlayer;
        }

        public override void Enter()
        {
            base.Enter();
            if (Satyr.RightHand == null) StateMachine.SetState(StateMachine.SearchingWeaponState);
        }

        public override void Process()
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            if (_isAttacking) return;
            
            StateMachine.SetDestination(Player.Instance.transform.position, _stopDistanceToPlayer, () =>
            {
                StateMachine.StartCoroutine(StartAttack());
            });
        }

        private IEnumerator StartAttack()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(Satyr.RightHand.WeaponDataSO.AttackDuration);
            Attack();
            
            Satyr.StartCoroutine(Utils.MakeActionDelay(
                () => { _isAttacking = false; },
                Satyr.RightHand.WeaponDataSO.AttackDelay));
        }

        private void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(
                _attackPoint.position, 
                Satyr.RightHand.WeaponDataSO.AttackRange);

            foreach (var col in colliders)
            {
                if (col.isTrigger || !col.TryGetComponent<Player>(out var player)) continue;
                
                player.HealthProperty.Value -= Satyr.RightHand.WeaponDataSO.Damage;
                
                if (Player.Instance.IsBlocking)
                {
                    player.HealthProperty.Value +=
                        Satyr.RightHand.WeaponDataSO.Damage *
                        (Player.Instance.LeftHand.ShieldDataSO.ReduceDamagePercentage / 100);
                }
                
                return;
            }
        }
        
    }
}