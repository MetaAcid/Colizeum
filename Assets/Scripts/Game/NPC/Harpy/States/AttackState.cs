using System.Collections;
using Common;
using UnityEngine;

namespace Game.NPC.Harpy.States
{
    public class AttackState : HarpyState
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
            
            Harpy.transform.LookAt(PlayerMechanics.Player.Instance.transform);
        }

        private void MoveToPlayer()
        {
            if (_isAttacking) return;
            
            StateMachine.SetDestination(PlayerMechanics.Player.Instance.transform.position + Vector3.up * 2, _stopDistanceToPlayer, () =>
            {
                StateMachine.StartCoroutine(StartAttack());
            });
        }

        private IEnumerator StartAttack()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(Harpy.RightHand.WeaponDataSO.AttackDuration);
            Attack();
            
            Harpy.StartCoroutine(Utils.MakeActionDelay(
                () => { _isAttacking = false; },
                Harpy.RightHand.WeaponDataSO.AttackDelay));
        }

        private void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(
                _attackPoint.position, 
                Harpy.RightHand.WeaponDataSO.AttackRange);

            foreach (var col in colliders)
            {
                if (col.isTrigger || !col.TryGetComponent<PlayerMechanics.Player>(out var player)) continue;
                
                player.HealthProperty.Value -= Harpy.RightHand.WeaponDataSO.Damage;
                
                if (PlayerMechanics.Player.Instance.IsBlocking)
                {
                    player.HealthProperty.Value +=
                        Harpy.RightHand.WeaponDataSO.Damage *
                        (PlayerMechanics.Player.Instance.LeftHand.ShieldDataSO.ReduceDamagePercentage / 100);
                }
                
                return;
            }
        }
    }
}