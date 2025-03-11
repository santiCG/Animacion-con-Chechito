using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterAim : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private CinemachineCamera aimingCamera;
    [SerializeField] private AimConstraint aimConstraint;
    [SerializeField] private Animator anim;
    [SerializeField] private FloatDampener aimDampener;

    public Character ParentCharacter { get; set; }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (!context.started && !context.canceled) return;

        aimingCamera?.gameObject.SetActive(context.started);
        ParentCharacter.IsAiming = context.started;
        aimConstraint.enabled = context.started;

        aimDampener.TargetValue = context.started ? 1 : 0;
    }

    private void Update()
    {
        aimDampener.Update();

        aimConstraint.weight = aimDampener.CurrentValue;
        anim.SetLayerWeight(1, aimDampener.CurrentValue);
        anim.SetLayerWeight(2, aimDampener.CurrentValue);
    }
}
