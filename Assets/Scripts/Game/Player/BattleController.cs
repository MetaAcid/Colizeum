using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleController : MonoBehaviour
{   
    public KeyCode takeButton;
    public OnSelect item;
    public ItemInHands handsItem;
    [FormerlySerializedAs("handAttack")] public int handAttackState;
    public float timer;
    public Animator anim;
    private int _battleState;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();
        anim.SetInteger("battleState", _battleState);
    }

    void FixedUpdate()
    {
        SeeObject();
    }

    private void SeeObject()
    {
        if (Input.GetKeyDown(takeButton))
        {
            Ray R = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height /2));
            RaycastHit hit;
            if (Physics.Raycast(R, out hit, 2f))
            {
                if (hit.collider.GetComponent<OnSelect>()) 
                {
                    Destroy(hit.collider.GetComponent<OnSelect>().gameObject);
                }
            }
        }
    }

    private void GetInput()
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

        if(anim.GetInteger("AttackHands") >= 1)
        {
            StartCoroutine(TimerAttack(0.7f));
        }
    }

    IEnumerator TimerAttack(float time)
    {
        timer += Time.deltaTime;
        if (timer>=time && timer != 0)
        {
            timer = 0;
            anim.SetInteger("AttackHands", 0);
            yield return null;
        }
    }
}
