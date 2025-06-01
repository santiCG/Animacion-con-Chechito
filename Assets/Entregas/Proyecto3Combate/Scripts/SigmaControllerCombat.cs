using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class SigmaMovementCombat : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float jogSpeed = 5f;

    [Header("Dampeners")]
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Animator animator;
    private Rigidbody rb;

    private bool isRunning;
    private bool isJogging;
    private bool canJump = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleAnimation();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float controlWeight = animator.GetFloat("WeightControl");

        if (controlWeight < 0.95f) return;

        isJogging = moveInput.magnitude > 0.7f && !isRunning ? true : false;

        if(moveInput.magnitude < 0.05f) isRunning = false;

        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        Vector3 desiredMoveDirection = (transform.right * moveDirection.x + transform.forward * moveDirection.z);

        float speed = isJogging ? jogSpeed : walkSpeed;

        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + desiredMoveDirection * speed * Time.deltaTime);
        }
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

        animator.SetFloat("SpeedX", speedX.CurrentValue < 0.05f && speedX.CurrentValue > -0.05f ? 0 : speedX.CurrentValue);
        animator.SetFloat("SpeedY", speedY.CurrentValue < 0.05f && speedY.CurrentValue > -0.05f ? 0 : speedY.CurrentValue);

        //animator.SetBool("IsRunning", isRunning);
        //animator.SetBool("IsJogging", isJogging);
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

    public void OnRevive()
    {
        animator.SetTrigger("Revive");
    }
}
