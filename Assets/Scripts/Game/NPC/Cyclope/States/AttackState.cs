using System.Collections;
using Common;
using UnityEngine;

namespace Game.NPC.Cyclope.States
{
    public class AttackState : CyclopeState
    {
        private readonly float _stopDistanceToPlayer;
        private readonly Transform _attackPoint;
        private bool _isAttacking = false;

        public AttackState(Transform attackPoint, float stopDistanceToPlayer)
        {
            _attackPoint = attackPoint;
            _stopDistanceToPlayer = stopDistanceToPlayer;
        }

        public override void Process()
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            if (_isAttacking) return;
            
            StateMachine.SetDestination(PlayerMechanics.Player.Instance.transform.position, _stopDistanceToPlayer, () =>
            {
                StateMachine.StartCoroutine(StartAttack());
            });
        }

        private IEnumerator StartAttack()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(Cyclope.RightHand.WeaponDataSO.AttackDuration);
            Attack();
            
            Cyclope.StartCoroutine(Utils.MakeActionDelay(
                () => { _isAttacking = false; },
                Cyclope.RightHand.WeaponDataSO.AttackDelay));
        }

        private void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(
                _attackPoint.position, 
                Cyclope.RightHand.WeaponDataSO.AttackRange);

            foreach (var col in colliders)
            {
                if (col.isTrigger || !col.TryGetComponent<PlayerMechanics.Player>(out var player)) continue;
                
                player.HealthProperty.Value -= Cyclope.RightHand.WeaponDataSO.Damage;
                
                if (PlayerMechanics.Player.Instance.IsBlocking)
                {
                    player.HealthProperty.Value +=
                        Cyclope.RightHand.WeaponDataSO.Damage *
                        (PlayerMechanics.Player.Instance.LeftHand.ShieldDataSO.ReduceDamagePercentage / 100);
                }
                
                return;
            }
        }
    }
}