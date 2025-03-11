using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class WeaponActionControllerAV : MonoBehaviour
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

        //Activar el sistema de disparo

        //Machetazo
        //Spawn Projectile
        // Mover proyectil

        //Robusto
        //Acceder a algun nexo de datos que se refieran al arma (puede ser un componente especifico para el arma que tenga equipada personaje
        // con componente arma, se activa su funcion de accionarse

    }
}
