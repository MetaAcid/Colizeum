using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image staminaBar;

    private float playerMaxStamina = 1f;
    private float playerCurrentStamina;

    private WaitForSeconds regTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private void Awake() 
    {
        instance = this;
    }
    
    void Start()
    {
        playerCurrentStamina = playerMaxStamina;
        staminaBar.fillAmount = playerMaxStamina;

    }

    public void UseStamina(float amount)
    {
        if(playerCurrentStamina - amount >= 0)
        {
            playerCurrentStamina -= amount;
            staminaBar.fillAmount = playerCurrentStamina;

            if(regen != null)
            {
                StopCoroutine(regen);
            }

            StartCoroutine(RegStamina());
        }
    }

    private IEnumerator RegStamina()
    {
        yield return new WaitForSeconds(2);

        while(playerCurrentStamina < playerMaxStamina)
        {
            playerCurrentStamina += 0.01f;
            staminaBar.fillAmount = playerCurrentStamina;
            yield return regTick;
        }
        regen = null;
    }
    
}
