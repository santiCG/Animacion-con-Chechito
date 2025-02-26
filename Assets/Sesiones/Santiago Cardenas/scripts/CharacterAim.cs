using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterAim : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private CinemachineCamera aimingCamera;
    [SerializeField] private AimConstraint aimConstraint;
    [SerializeField] private Animator anim;

    public Character ParentCharacter { get; set; }

    public void OnAim(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            aimingCamera?.gameObject.SetActive(true);
            ParentCharacter.IsAiming = true;
            aimConstraint.weight = 1f;
            anim.SetLayerWeight(1, 1);
        }

        if(context.canceled)
        {
            aimingCamera?.gameObject.SetActive(false);
            ParentCharacter.IsAiming = false;
            aimConstraint.weight = 0f;
            anim.SetLayerWeight(1, 0);
        }
    }
}
