using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar staminaBar;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private bool IsMouseVisible;

    [Header("Animation Settings")]
    [SerializeField] private float healthBarSpeed = 3f;
    [SerializeField] private float staminaBarSpeed = 5f; // Más rápido para mejor feedback

    public void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }

        IsMouseVisible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            AlternateMouseVisibility();
        }
    }

    private void AlternateMouseVisibility()
    {
        IsMouseVisible = !IsMouseVisible;
        pauseMenu.SetActive(IsMouseVisible);

        if (IsMouseVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
