using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
}
