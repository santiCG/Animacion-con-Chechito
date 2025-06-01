using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterMovement2AV : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private FloatDampener1 speedX;
    [SerializeField] private FloatDampener1 speedY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMotionVector(float targetX, float targetY)
    {
        speedX.TargetValue = targetX;
        speedY.TargetValue = targetY;

    }
    
    private void ApplyMotion()
    {
        speedX.Update();
        speedY.Update();

        animator.SetFloat("SpeedX", speedX.CurrentValue);
        animator.SetFloat("SpeedY", speedY.CurrentValue);
    }

    private void Update()
    {
        if (animator.updateMode != AnimatorUpdateMode.Normal) return;

        ApplyMotion();
        
    }

    private void FixedUpdate()
    {
        if (animator.updateMode != AnimatorUpdateMode.Normal) return;

        ApplyMotion();

    }
}
