using UnityEngine;

/// <summary>
/// Stores all dynamic variables for the character
/// </summary>
public class CharacterStateAV : MonoBehaviour
{
    [SerializeField] private float startStamina = 100;
    [SerializeField] private float staminaRegen = 2;
    [SerializeField] private float startHealth = 100;

    [SerializeField] private float currentStamina;
    [SerializeField] private float currentHealth;

    // Evento que se dispara cuando cambia la salud
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    public delegate void StaminaChangedDelegate(float currentStamina, float maxStamina);
    public event StaminaChangedDelegate OnStaminaChanged;

    public float CurrentStamina { get { return currentStamina; } }

    private void Awake()
    {
        currentStamina = startStamina;
        currentHealth = startHealth;

    }

    public void Start()
    {
        if (UIManager.Singleton == null) return;

        OnHealthChanged += UIManager.Singleton.GetPlayerHealth;
        OnStaminaChanged += UIManager.Singleton.GetPlayerStamina;

        OnHealthChanged?.Invoke(currentHealth, startHealth);
        OnStaminaChanged?.Invoke(currentStamina, startStamina);
    }

    private void Update()
    {
        if(currentStamina < startStamina)
        {
            RegenStamina(staminaRegen * Time.deltaTime);   
        }
    }

    private void RegenStamina(float regenAmount)
    {
        float newStamina = Mathf.Min(currentStamina + regenAmount, startStamina);
        if (newStamina != currentStamina)
        {
            currentStamina = newStamina;
            OnStaminaChanged?.Invoke(currentStamina, startStamina);
        }
    }

    public void DepleteStamina(float amount)
    {
        currentStamina -= GetStaminaDepletion() * amount;
        OnStaminaChanged?.Invoke(currentStamina, startStamina);

    }

    public void DepleteStaminaWithParameter(string parameter)
    {
        float motionValue = GetComponent<Animator>().GetFloat(parameter);
        DepleteStamina(motionValue);
        OnStaminaChanged?.Invoke(currentStamina, startStamina);
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
        // Disparar evento cuando cambia la salud
        OnHealthChanged?.Invoke(currentHealth, startHealth);
        if (currentHealth <= 0)
        {
            #warning ToDo death
            //Se murio
            zeroHealth = true;

        }
    }


}
