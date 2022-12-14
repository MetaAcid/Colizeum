using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerL : MonoBehaviour
{
    public GameObject Shield;
    public GameObject Player;
    public bool CanBlockWeapon = true;
    public float BlockCooldown = 1.0f;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(1))
        {   
            if(CanBlockWeapon)
            {
                ShieldBlock();
            }
        }    
    }

    public void ShieldBlock()
    {
        CanBlockWeapon = false;
        Animator anim = Player.GetComponent<Animator>();
        anim.SetTrigger("Block");
        StartCoroutine(ResetBlockCooldown());
    }

    IEnumerator ResetBlockCooldown()
    {
        yield return new WaitForSeconds(BlockCooldown);
        CanBlockWeapon = true;
    }
}
