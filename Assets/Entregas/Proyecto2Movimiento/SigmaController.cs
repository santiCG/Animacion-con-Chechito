using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class SigmaMovement : MonoBehaviour
{
    [Header("Dampeners")]
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Animator animator;

    private bool isRunning;
    private bool isJogging;
    private bool canJump = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAnimation();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        isJogging = moveInput.magnitude > 0.7f && !isRunning ? true : false;

        if(moveInput.magnitude < 0.05f) isRunning = false;
    }

    private void HandleRotation()
    {
        if (moveInput.magnitude > 0.05f)
        {
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleAnimation()
    {
        speedX.Update();
        speedY.Update();

        speedX.TargetValue = moveInput.x;
        speedY.TargetValue = moveInput.y;

        animator.SetFloat("SpeedX", speedX.CurrentValue);
        animator.SetFloat("SpeedY", speedY.CurrentValue);

        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJogging", isJogging);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = !isRunning;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            animator.SetTrigger("Jump");
            canJump = false;

            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float jumpDuration = moveInput.magnitude > 0.05f ? 1.2f : 2;
        yield return new WaitForSeconds(jumpDuration);
        canJump = true;
    }
}
