using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharactercitoMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.updateMode != AnimatorUpdateMode.Normal) return;

        ApplyMotion();
    }
    private void FixedUpdate()
    {
        if (animator.updateMode != AnimatorUpdateMode.Fixed) return;

        ApplyMotion();
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
}
