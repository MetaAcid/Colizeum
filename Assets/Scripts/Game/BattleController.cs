using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{   
    public int handAttack;
    public int shoot;
    public float timer;
    public Animator anim;
    private int _battleState;
    public ControlItemInInventory item;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        anim.SetInteger("battleState", _battleState);
    }

    private void GetInput()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (timer <= 0.01f)
            {
                if (handAttack != 2){
                    handAttack += 1;
                }
                else
                {
                    handAttack = 1;
                }
                
                // if(GetComponent<StatsPlay> ().RightHand == null) 
                // {
                //     anim.SetInteger("AttackHands", handAttack);
                // }
                // else
                // {
                //     anim.SetInteger("AttackWeapon", handAttack);
                // }
            }
            if(item.isActive == true)
            {
                _battleState = 2;
            }
            else
            {
                _battleState = 1;
            }
        }

        if(anim.GetInteger("AttackHands") >= 1)
        {
            StartCoroutine(TimerAttack(0.7f));
        }

        if(Input.GetMouseButtonUp(2))
        {
            _battleState = 3;
        }
    }

    IEnumerator TimerAttack(float time)
    {
        timer = timer + Time.deltaTime;
        if(timer>=time && timer != 0)
        {
            timer = 0;
            anim.SetInteger("AttackHands", 0);
            anim.SetInteger("AttackWeapon", 0);
            yield return null;
        }
    }
}
