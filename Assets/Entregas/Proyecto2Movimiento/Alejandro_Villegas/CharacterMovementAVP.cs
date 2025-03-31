using UnityEngine;
using UnityEngine.InputSystem;

/*
[] atributos

 */
[RequireComponent(typeof(Animator))]
public class CharacterMovementAVP : MonoBehaviour, ICharacterComponentAVP
{
    /* [SerializeField] private float speedX;
     [SerializeField] private float speedY; */

    [SerializeField] private Camera camera;
    [SerializeField] private FloatDampener2 speedX;
    [SerializeField] private FloatDampener2 speedY;
    [SerializeField] private float angularSpeed;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float rotationThreshold;
    private Animator animator;

    private int speedXHash; //Id para comparar un entero en vez de un string
    private int speedYHash;

    private Quaternion targetRotation;

    public CharacterAVP ParentCharacter { get; set;}

    private void SolveCharacterRotation()
    {
        Vector3 floorNormal = transform.up;
        Vector3 cameraRealForward = camera.transform.forward;
        float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));
        Vector3 cameraForward = Vector3.Lerp(cameraRealForward, camera.transform.up, angleInterpolator).normalized;
        Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;
        Debug.DrawLine(transform.position, transform.position + characterForward * 2, Color.magenta, 5);
        targetRotation = Quaternion.LookRotation(characterForward, floorNormal);
        
    }

    private void ApplyCharacterRotation()
    {
        float motionMagnitude = Mathf.Sqrt(speedX.TargetValue * speedX.TargetValue + speedY.TargetValue * speedY.TargetValue);
        float rotationSpeed = /*ParentCharacter.IsAiming? 1 : */Mathf.SmoothStep(0, 0.1f, motionMagnitude);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
    }

    private void ApplyCharacterRotationFromAim()
    {
        Vector3 aimForward = Vector3.ProjectOnPlane(aimTarget.forward, transform.up).normalized;
        Vector3 characterForward = transform.forward;
        float angleCos = Vector3.Dot(characterForward, aimForward);
        float rotationSpeed = 0;

        //rotationSpeed = (1 - (angleCos * 0.5f + 0.5f)) /Mathf.Sin(rotationThreshold);
        rotationSpeed = Mathf.SmoothStep(0f, 1f, Mathf.Acos(angleCos) * Mathf.Rad2Deg / rotationThreshold);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
      Vector2 inputValue = context.ReadValue<Vector2>();

        if(ParentCharacter.IsRunning)
        {
            speedX.TargetValue = inputValue.x;
            speedY.TargetValue = inputValue.y;
        }
        else
        {
            speedX.TargetValue = (inputValue.x / 3) * 2;
            speedY.TargetValue = (inputValue.y / 3) * 2;
        }
        SolveCharacterRotation();
        /*if (inputValue.magnitude > .1f)
            SolveCharacterRotation();*/

        //animator.SetFloat(speedXHash, inputValue.x);
        //animator.SetFloat(speedYHash, inputValue.y);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (ParentCharacter.IsGrounded)
            {
                animator.SetTrigger("Jump");
            }
        }

    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (ParentCharacter.IsGrounded)
            {
                ParentCharacter.IsRunning = !ParentCharacter.IsRunning;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedXHash = Animator.StringToHash("SpeedX");
        speedYHash = Animator.StringToHash("SpeedY");

    }

    private void OnValidate() //Solo sirve en editor
    {


    }

    private void Update()
    {
        speedX.Update();
        speedY.Update();
        if (ParentCharacter.IsGrounded )
        {
            animator.SetFloat(speedXHash, speedX.CurrentValue);
            animator.SetFloat(speedYHash, speedY.CurrentValue);
        }
        if (!ParentCharacter.IsAiming)
            ApplyCharacterRotation();
        else
            ApplyCharacterRotationFromAim();

    }

}
