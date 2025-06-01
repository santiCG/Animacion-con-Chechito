using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar staminaBar;

    [Header("Animation Settings")]
    [SerializeField] private float healthBarSpeed = 3f;
    [SerializeField] private float staminaBarSpeed = 5f; // Más rápido para mejor feedback

    public void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }
    public void GetPlayerHealth(float playerHealth, float maxHealth)
    {
        healthBar.SetProgress(playerHealth/maxHealth, healthBarSpeed);
    }

    public void GetPlayerStamina(float playerStamina, float maxStamina)
    {
        staminaBar.SetProgress(playerStamina/maxStamina, staminaBarSpeed);
    }
}
