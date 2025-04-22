using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    private Animator anim;
    private AttackHitboxController hitboxController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitboxController = GetComponent<AttackHitboxController>();
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if(context.performed && Game.Instance.PlayerOne.GetCurrentStamina > 0)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled && Game.Instance.PlayerOne.GetCurrentStamina > 0)
        {
            anim.SetTrigger("HeavyAttack");
        }
    }

    public void DepleteStamina(float amount)
    {
        Game.Instance.PlayerOne.DepleteStamina(amount);
    }

    public void ToggleAttackHitbox(int hitboxID)
    {
        hitboxController.ToggleHitboxes(hitboxID);
    }

    public void CleanupAttackHitbox() 
    {
        hitboxController.CleanupHitboxes();
    }
}
