using System.Collections;
using Common;
using Entities.Weapons;
using UnityEngine;


namespace Game.PlayerMechanics.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private float timer;
        [SerializeField] private int handAttackState;
        
        public Animator animator;


        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        
        public void AttackWithHands()
        {
            if(Input.GetMouseButtonUp(0))
            {
                if (timer <= 0.01f)
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
            }

            if (animator.GetInteger(AnimationsConfig.AnimationAttackHands) >= 1)
            {
                StartCoroutine(TimerAttack(0.7f));
            }
        }

        public void AttackWithWeapon(WeaponDataSO weaponDataSO)
        {
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetTrigger(AnimationsConfig.AnimationAttackWeapon);
            }
        }

        public void BlockWithShield(WeaponDataSO weaponDataSO)
        {
            if (Input.GetMouseButtonUp(1))
            {
                animator.SetTrigger(AnimationsConfig.AnimationBlock);
            }
        }
        
        IEnumerator TimerAttack(float time)
        {
            timer += Time.deltaTime;
            if (timer>=time && timer != 0)
            {
                timer = 0;
                animator.SetInteger(AnimationsConfig.AnimationAttackHands, 0);
                yield return null;
            }
        }
    }
}