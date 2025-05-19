using UnityEngine;

public class ChracterStateALR : MonoBehaviour
{

    [SerializeField] private float startStamina = 100;
    [SerializeField] private float staminaRegen = 5;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float currentHealth;

    [SerializeField] private float currentStamina;

    private void Start()
    {
        currentStamina = startStamina;
        currentHealth = startHealth;
    }

    private float GetStaminDepletion()
    {
        return 60;
    }

    private void RegenerateStamina(float amount)
    {
        currentStamina = Mathf.Min(currentStamina + amount, startStamina);
    }

    public void DepleteStamina(float amount)
    {
        currentStamina = GetStaminDepletion() * amount;
    }

    public void DepleteHealth(float amount, out bool zeroHealth)
    {
        currentHealth -= amount;
        zeroHealth = false;

        if (currentHealth <= 0) 
        {
            #warning ToDo: Death
            zeroHealth = true;
        }
    }

    //public void DepleteStaminaWithParam(string param)
    //{
    //    float motionValue = GetComponent<Animator>().GetFloat(param);
    //    DepleteStamina(motionValue);
    //}

    private void Update()
    {
        RegenerateStamina(staminaRegen * Time.deltaTime);
    }

    public float GetCurrentStamina => currentStamina;
}