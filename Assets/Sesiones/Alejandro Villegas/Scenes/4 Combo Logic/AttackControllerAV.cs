using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

[RequireComponent(typeof(Animator))]
public class AttackControllerAV : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnLightAttack(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void OnHeavyAttack(CallbackContext ctx) 
    {
        if (ctx.performed || ctx.canceled)
        {
            anim.SetTrigger("HeavyAttack");
        }

    }
}
