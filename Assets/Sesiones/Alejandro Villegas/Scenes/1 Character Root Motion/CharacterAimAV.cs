using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterAimAV : MonoBehaviour, ICharacterComponentAV
{
    [SerializeField] private CinemachineCamera aimingCamera;
    [SerializeField] private AimConstraint aimConstraint;
    [SerializeField] private FloatDampener1 aimDampener;
    private Animator anim;

    public CharacterAV ParentCharacter {get; set;}

    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (!ctx.started && !ctx.canceled) return;

        aimingCamera?.gameObject.SetActive(ctx.started);
        ParentCharacter.IsAiming = ctx.started;
        aimConstraint.enabled = ctx.started;
        aimDampener.TargetValue = ctx.started ? 1:0;

       /* if (ctx.started)
        {
            //Apuntar
            aimingCamera?.gameObject.SetActive(true);
            ParentCharacter.IsAiming = true;
            aimDampener.TargetValue = 1;
            aimConstraint.weight = 1f;
            anim.SetLayerWeight(1, 1);
        }

        if (ctx.canceled)
        {
            //Dejar de apuntar
            aimingCamera?.gameObject?.SetActive(false);
            ParentCharacter.IsAiming = false;
            aimDampener.TargetValue = 0;
            aimConstraint.weight = 0f;
            anim.SetLayerWeight(1, 0);

        }*/
    }
    public void Update()
    {
        aimDampener.Update();
        aimConstraint.weight = aimDampener.CurrentValue;
        anim.SetLayerWeight(1, aimDampener.CurrentValue);
    }


}
