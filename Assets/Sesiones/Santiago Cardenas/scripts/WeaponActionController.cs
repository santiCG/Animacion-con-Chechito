using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class WeaponActionController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnWeaponAction(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;

        animator.SetTrigger("WeaponAction");
    }
}
