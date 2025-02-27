using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private new Camera camera;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;
    [SerializeField] private float angularSpeed;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float rotationThreshold;
    
    private Animator animator;

    private int speedXHash;
    private int speedYHash;

    private Quaternion targetRotation;

    private void SolveCharacterRotation()
    {
        Vector3 floorNormal = transform.up;
        Vector3 cameraRealForward = camera.transform.forward;
        float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));
        Vector3 cameraForward = Vector3.Lerp(cameraRealForward, camera.transform.up, angleInterpolator).normalized;
        Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;
        Debug.DrawLine(transform.position, transform.position + characterForward * 2, Color.magenta,5);
        targetRotation = Quaternion.LookRotation(characterForward, floorNormal);
    }

    private void ApplyCharacterRotation()
    {
        float motionMagnitude = Mathf.Sqrt(speedX.TargetValue * speedX.TargetValue + speedY.TargetValue * speedY.TargetValue);
        float rotationSpeed = ParentCharacter.IsAiming ? 1 : Mathf.SmoothStep(0, .1f, motionMagnitude);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
    }

    private void ApplyCharacterRotFromAim()
    {
        Vector3 aimFor = Vector3.ProjectOnPlane(aimTarget.forward, transform.up).normalized;
        Vector3 characterFor = transform.forward;
        float angleCos = Vector3.Dot(characterFor, aimFor);
        float rotSpeed = Mathf.SmoothStep(0, 1, Mathf.Acos(angleCos) * Mathf.Rad2Deg / rotationThreshold);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotSpeed);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        speedX.TargetValue = inputValue.x;
        speedY.TargetValue = inputValue.y;
    }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedXHash = Animator.StringToHash("SpeedX");
        speedYHash = Animator.StringToHash("SpeedY");
    }

    private void Update()
    {
        speedX.Update();
        speedY.Update();
        animator.SetFloat(speedXHash, speedX.CurrentValue);
        animator.SetFloat(speedYHash, speedY.CurrentValue);
        SolveCharacterRotation();

        if(!ParentCharacter.IsAiming)
        {
            ApplyCharacterRotation();
        }
        else
        {
            ApplyCharacterRotFromAim();
        }
    }

    public Character ParentCharacter { get; set; }
}
