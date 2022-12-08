using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Image healthBar;
    public Image staminaBar;
    public int playerHealth;
    private float staminaRunReduction;
    private float staminaWalkReduction;
    private int _state;
    public float speed = 7f;
    private float maxSpeed = 10f;
    public float jumpPower = 200f;
    public bool ground;
    public Rigidbody rb;
    public float timer;
    public int handAttack;

    // Start is called before the first frame update
    void Start()
    {  
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        staminaRunReduction -= Time.deltaTime * 0.3f;
        staminaWalkReduction -= Time.deltaTime * 0.1f;
        HealthCalc();
        GetInput();
        anim.SetInteger("state", _state);
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //playerStamina -= Time.deltaTime * 0.5f;
                //staminaBar.fillAmount = playerStamina;
                StaminaBar.instance.UseStamina(staminaRunReduction);
                _state = 2;
                transform.localPosition += transform.forward * maxSpeed * Time.deltaTime;
            }
            else
            {
                //playerStamina += Time.deltaTime * 0.1f;
                //staminaBar.fillAmount = playerStamina;
                StaminaBar.instance.UseStamina(staminaWalkReduction);
                _state = 1;
                transform.localPosition += transform.forward * speed * Time.deltaTime;
            }
        } 
        else
        {
            _state = 0;
        }
        

        if(Input.GetKey(KeyCode.S))
        {
            _state = 6;
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }
        

        if(Input.GetKey(KeyCode.A))
        {
            _state = 3;
            transform.localPosition += -transform.right * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            _state = 4;
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ground == true)
            {   
                if(Input.GetKey(KeyCode.LeftShift)&&Input.GetKey(KeyCode.W)){
                    _state = 7;
                    StaminaBar.instance.UseStamina(0.2f);
                    rb.AddForce(transform.up * jumpPower * 1.1f);
                }
                else
                {
                    _state = 5;
                    StaminaBar.instance.UseStamina(0.1f);
                    rb.AddForce(transform.up * jumpPower);
                }
                
                
            }
        }

        if(Input.GetMouseButtonDown(0))
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
                anim.SetInteger("AttackHands", handAttack);
            }
        }

        if(anim.GetInteger("AttackHands") >= 1)
        {
            StartCoroutine(TimerAttack(0.7f));
        }
    }

    IEnumerator TimerAttack(float time)
    {
        timer = timer + Time.deltaTime;
        if(timer>=time && timer != 0)
        {
            timer = 0;
            anim.SetInteger("AttackHands", 0);
            //anim.SetInteger("AttackWeapon", 0);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }

    private void HealthCalc()
    {
        float currentHealth = playerHealth/100;

        if(currentHealth < 1.0)
        {
            healthBar.fillAmount = currentHealth;
        }
    }
}
