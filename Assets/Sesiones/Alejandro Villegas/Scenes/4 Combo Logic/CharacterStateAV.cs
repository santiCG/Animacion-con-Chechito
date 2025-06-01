using UnityEngine;

/// <summary>
/// Stores all dynamic variables for the character
/// </summary>
public class CharacterStateAV : MonoBehaviour
{
    [SerializeField] private float startStamina = 100;
    [SerializeField] private float staminaRegen = 100;
    [SerializeField] private float startHealth = 100;

    [SerializeField] private float currentStamina;
    [SerializeField] private float currentHealth;

    public float CurrentStamina { get { return currentStamina; } }

    private void Awake()
    {
        currentStamina = startStamina;
        currentHealth = startHealth;
    }

    private void Update()
    {
        RegenStamina(staminaRegen * Time.deltaTime);   
    }

    private void RegenStamina(float regenAmount)
    {
        currentStamina = Mathf.Min(currentStamina + regenAmount, startStamina);
    }

    public void DepleteStamina(float amount)
    {
        currentStamina -= GetStaminaDepletion() * amount;
    }

    public void DepleteStaminaWithParameter(string parameter)
    {
        float motionValue = GetComponent<Animator>().GetFloat(parameter);
        DepleteStamina(motionValue);
    }

    private float GetStaminaDepletion()
    {
        // Sistema de inventario * 1 / stat fuerza * 1/buff_fuerza

        return 60;
    }

    public void DepleteHealth(float amount, out bool zeroHealth)
    {
        zeroHealth = false;
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            #warning ToDo death
            //Se murio
            zeroHealth = true;

        }
    }

}
