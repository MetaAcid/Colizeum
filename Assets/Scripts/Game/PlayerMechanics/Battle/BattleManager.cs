using System.Collections;
using Common;
using Entities.Weapons;
using Game.NPC;
using Game.NPC.Cyclope;
using Game.NPC.Harpy;
using UnityEngine;
using UnityEngine.Serialization;


namespace Game.PlayerMechanics.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private float handsAnimationTimer;
        [SerializeField] private int handAttackState;
        [SerializeField] private Transform attackPoint;
        
        public Animator animator;


        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        
        public void AttackWithHands()
        {
            if (handsAnimationTimer <= 0.01f)
            {
                if (handAttackState != 2)
                {
                    handAttackState += 1;
                }
                else
                {
                    handAttackState = 1;
                }
            }

            if (animator.GetInteger(AnimationsConfig.AnimationAttackHands) >= 1)
            {
                StartCoroutine(TimerAttack(0.7f));
            }
        }

        public void AttackWithWeapon(WeaponDataSO weaponDataSO)
        {
            animator.SetTrigger(AnimationsConfig.AnimationAttackWeapon);
            Attack(weaponDataSO);
        }

        public void SetBlockShieldStatus(bool isBlocked)
        {
            Player.Instance.IsBlocking = isBlocked;
            
            if (isBlocked)
            {
                animator.SetTrigger(AnimationsConfig.AnimationBlock);
                Debug.Log("Block!");
                return;
            }    
            
            Debug.Log("Unblock");
        }
        
        public void BlockWithShield(WeaponDataSO weaponDataSO)
        {
        }

        private void Attack(WeaponDataSO weapon)
        {
            Collider[] colliders = Physics.OverlapSphere(
                attackPoint.position, 
                weapon.AttackRange);

            foreach (var col in colliders)
            {
                if (col.GetComponent<Satyr>())
                {
                    col.GetComponent<Satyr>().HealthProperty.Value -= weapon.Damage;
                }
                else if (col.GetComponent<Cyclope>())
                {
                    col.GetComponent<Cyclope>().HealthProperty.Value -= weapon.Damage;
                }
                else if (col.GetComponent<Harpy>())
                {
                    col.GetComponent<Harpy>().HealthProperty.Value -= weapon.Damage;
                }
            }
        }
        
        IEnumerator TimerAttack(float time)
        {
            handsAnimationTimer += Time.deltaTime;
            if (handsAnimationTimer>=time && handsAnimationTimer != 0)
            {
                handsAnimationTimer = 0;
                animator.SetInteger(AnimationsConfig.AnimationAttackHands, 0);
                yield return null;
            }
        }
    }
}