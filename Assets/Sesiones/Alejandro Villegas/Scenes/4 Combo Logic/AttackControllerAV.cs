using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;



[RequireComponent(typeof(Animator))]
//[RequireComponent (typeof(CharacterStateAV))]
public class AttackControllerAV : MonoBehaviour
{

    [SerializeField] private float lightAttackCost;
    [SerializeField] private float heavyAttackCost;
    AttackHitboxControllerAV hitboxController;

    private Animator anim;
  //  CharacterStateAV characterState;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitboxController = GetComponent<AttackHitboxControllerAV>();
    //    characterState = GetComponent<CharacterStateAV>();
    }

    private bool CanAttack()
    {
        return anim.GetFloat("ControlWeights") > 0;
    }

    public void OnLightAttack(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(GameAV.Instance.PlayerOne.CurrentStamina > 0)
            {
                anim.SetTrigger("Attack");
            }


        }
    }

    public void OnHeavyAttack(CallbackContext ctx) 
    {
        if (ctx.performed || ctx.canceled)
        {
            if (GameAV.Instance.PlayerOne.CurrentStamina > 0)
            {
                anim.SetTrigger("HeavyAttack");
            }

        }

    }

    public void DepleteStamina(float amount)
    {
        GameAV.Instance.PlayerOne.DepleteStamina(amount);
    }

    public void DepleteStaminaWithParameter(string parameter)
    {
        float motionValue = GetComponent<Animator>().GetFloat(parameter);
        DepleteStamina(motionValue);
    }

    public void ToggleAttackHitbox(int hitboxId)
    {
        // Make hitbox system
        hitboxController.ToggleHitboxes(hitboxId);
    }

    public void CleanUpAttackHitbox()
    {
        hitboxController.CleanUpHitboxes();
    }
}
