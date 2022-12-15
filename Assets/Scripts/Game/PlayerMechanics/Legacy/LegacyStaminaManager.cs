using System.Collections;
using System.Collections.Generic;
using Game.Player.Legacy;
using UnityEngine;
using UnityEngine.UI;

public class LegacyStaminaManager : MonoBehaviour
{
    const float MAX_STAMINA = 1f;

    [SerializeField]
    private Image staminaBar;
    [Header("Stamina usage (in stamina bar percentage)")]
    [Range(0, 1)]
    [SerializeField]
    private float jumpCost;
    [Range(0, 1)]
    [SerializeField]
    private float runPerSecondCost;
    [Header("Stamina regeneration")]
    [SerializeField]
    private float secondsUntilRegen;
    [Range(0, 1)]
    [SerializeField]
    private float regenPerSecond;


    private LegacyPlayerController _legacyPlayerController;

    private float currentStamina;
    private float staminaNotUsedTimer = 0f;

    void Awake()
    {
        _legacyPlayerController = transform.parent.gameObject.GetComponent<LegacyPlayerController>();
        currentStamina = MAX_STAMINA;
        UpdateStaminaBar();
    }

    private void OnEnable()
    {
        _legacyPlayerController.OnPlayerJump += UseStaminaToJump;
        _legacyPlayerController.OnPlayerRun += UseStaminaToRun;
    }

    private void OnDisable()
    {
        _legacyPlayerController.OnPlayerJump -= UseStaminaToJump;
        _legacyPlayerController.OnPlayerRun -= UseStaminaToRun;
    }

    void Update()
    {
        staminaNotUsedTimer += Time.deltaTime;
        if (StaminaShouldRegen())
        {
            RegenStamina();
        }
    }

    private void UseStamina(float amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Non-positive stamina change value was passed to StaminaManager!");
            return;
        }

        staminaNotUsedTimer = 0;

        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            UpdateStaminaBar();
        }
    }

    private void RegenStamina()
    {
        float regenAmount = regenPerSecond * Time.deltaTime;
        if (currentStamina + regenAmount <= MAX_STAMINA)
        {
            currentStamina += regenAmount;
            UpdateStaminaBar();
        }
    }

    private void UpdateStaminaBar()
    {
        staminaBar.fillAmount = currentStamina;
    }

    private bool StaminaShouldRegen()
    {
        if (staminaNotUsedTimer <= secondsUntilRegen)
        {
            return false;
        }
        return true;
    }

    private void UseStaminaToJump()
    {
        UseStamina(jumpCost);
    }

    private void UseStaminaToRun()
    {
        float runPerFrameCost = runPerSecondCost * Time.deltaTime;
        UseStamina(runPerFrameCost);
    }
}
