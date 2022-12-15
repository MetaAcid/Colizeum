using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollisionDetection : MonoBehaviour
{
    public WeaponControllerR wp;
    public GameObject HitParticle;
    private readonly int Hit = Animator.StringToHash("Hit");

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "enemy" || !wp.IsAttacking) return;
        
        other.GetComponent<Animator>().SetTrigger(Hit);
        Instantiate(
            HitParticle, 
            new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z),
            other.transform.rotation);
    }
}
