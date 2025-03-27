using System.Collections;
using Unity.VisualScripting;
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

    [Header("Jump")]
    [SerializeField] private float jumpDuration = 1.4f;
    [SerializeField] private float jumpCooldown = 1f;

    [Header("Ground Detection")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float GroundedOffset = -0.14f;
    [SerializeField] private float GroundedRadius = 0.28f;
    [SerializeField] private LayerMask GroundLayers;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 desiredMoveDirection;

    private Animator animator;

    private bool isRunning;
    private bool isJogging;
    private bool canJump;

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
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        desiredMoveDirection = (transform.right * moveDirection.x + transform.forward * moveDirection.z);

        isJogging = moveInput.x > 0.6f || moveInput.y > 0.6f ? true : false;

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
        canJump = isGrounded ? true : false;

        //if (context.performed && canJump)
        if (context.performed)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("IsJumping", true);
            canJump = false;

            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        //rb.linearVelocity = Vector3.zero;
        //rb.AddForce((desiredMoveDirection * jumpHorizontalForce) + (jumpVerticalForce * Vector3.up), ForceMode.Impulse);

        yield return new WaitForSeconds(jumpDuration);

        animator.SetBool("isJumping", false);
        canJump = true;
    }

    private void CheckGround()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
            GroundedRadius);
    }
}
