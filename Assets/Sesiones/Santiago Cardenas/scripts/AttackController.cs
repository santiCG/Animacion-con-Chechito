using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            anim.SetTrigger("HeavyAttack");
        }
    }
}
