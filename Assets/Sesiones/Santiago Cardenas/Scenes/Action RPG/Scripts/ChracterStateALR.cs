using UnityEngine;
using UnityEngine.UI;

public class ChracterStateALR : MonoBehaviour
{

    [SerializeField] private float startStamina = 100;
    [SerializeField] private float staminaRegen = 5;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float currentHealth;

    [SerializeField] private float currentStamina;
    [SerializeField] private Image youDie;

    // Evento que se dispara cuando cambia la salud
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    public delegate void StaminaChangedDelegate(float currentStamina, float maxStamina);
    public event StaminaChangedDelegate OnStaminaChanged;

    private void Awake()
    {
        currentStamina = startStamina;
        currentHealth = startHealth;
    }
    private void Start()
    {
        if (UIManager.Singleton == null) return;

        OnHealthChanged += UIManager.Singleton.GetPlayerHealth;
        OnStaminaChanged += UIManager.Singleton.GetPlayerStamina;

        OnHealthChanged?.Invoke(currentHealth, startHealth);
        OnStaminaChanged?.Invoke(currentStamina, startStamina);
    }

    private float GetStaminDepletion()
    {
        return 60;
    }

    private void RegenerateStamina(float amount)
    {
        float newStamina = Mathf.Min(currentStamina + amount, startStamina);
        if (newStamina != currentStamina)
        {
            currentStamina = newStamina;
            OnStaminaChanged?.Invoke(currentStamina, startStamina);
        }
    }

    public void DepleteStamina(float amount)
    {
        currentStamina = GetStaminDepletion() * amount;
        OnStaminaChanged?.Invoke(currentStamina, startStamina);
    }

    public void DepleteHealth(float amount, out bool zeroHealth)
    {
        currentHealth -= amount;
        zeroHealth = false;
        OnHealthChanged?.Invoke(currentHealth, startHealth);
        if (currentHealth <= 0) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            youDie.gameObject.SetActive(true);
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