using UnityEngine;
using UnityEngine.InputSystem;

/*
[] atributos

 */
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    /* [SerializeField] private float speedX;
     [SerializeField] private float speedY; */

    [SerializeField] private Camera camera;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;
    [SerializeField] private float angularSpeed;
    private Animator animator;

    private int speedXHash; //Id para comparar un entero en vez de un string
    private int speedYHash;

    private Quaternion targetRotation;

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

    public void OnMove(InputAction.CallbackContext context)
    {
      Vector2 inputValue = context.ReadValue<Vector2>();
        speedX.TargetValue = inputValue.x;
        speedY.TargetValue = inputValue.y;
        if (inputValue.magnitude > .1f)
            SolveCharacterRotation();

        //animator.SetFloat(speedXHash, inputValue.x);
        //animator.SetFloat(speedYHash, inputValue.y);
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

        animator.SetFloat(speedXHash, speedX.CurrentValue);
        animator.SetFloat(speedYHash, speedY.CurrentValue);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed);

    }

}
