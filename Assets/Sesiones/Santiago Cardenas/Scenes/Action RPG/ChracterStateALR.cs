using UnityEngine;

public class ChracterStateALR : MonoBehaviour
{

    [SerializeField] private float startStamina;
    [SerializeField] private float staminaRegen;
    [SerializeField] private float startHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] private float currentStamina;

    private void Start()
    {
        currentStamina = startStamina;
    }

    private float GetStaminDepletion()
    {
        //Sistema de inventario * 1/stat_fuerza * 1/buff_fuerza
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

    public void DepleteHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0) 
        {
            #warning ToDo: Death
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