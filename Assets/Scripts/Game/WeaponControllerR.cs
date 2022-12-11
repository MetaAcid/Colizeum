using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Axe;
    public bool CanAttackWeapon = true;
    public float AttackCooldown = 1.0f;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {   
            if(CanAttackWeapon)
            {
                AxeAttack();
            }
        }    
    }

    public void AxeAttack()
    {
        CanAttackWeapon = false;
        Animator anim = Axe.GetComponent<Animator>();
        anim.SetTrigger("AttackWeapon");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttackWeapon = true;
    }
}
