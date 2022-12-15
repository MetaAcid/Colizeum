using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerR : MonoBehaviour
{
    public GameObject Axe;
    public GameObject Player;
    public bool CanAttackWeapon = true;
    public float AttackCooldown = 1.0f;
    public bool IsAttacking = false;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0) && CanAttackWeapon)
        {   
            AxeAttack();
        }    
    }

    public void AxeAttack()
    {
        IsAttacking = true;
        CanAttackWeapon = false;
        Animator anim = Player.GetComponent<Animator>();
        anim.SetTrigger("AttackWeapon");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttackWeapon = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }
}
