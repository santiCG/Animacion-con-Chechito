using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sesiones.Santiago.Animation
{
    [RequireComponent(typeof(Animator))]

    public class AnimatorControls : MonoBehaviour
    {
        //[SerializeField][Range(-1f, 1f)] private float speedX;
        //[SerializeField][Range(-1f, 1f)] private float speedY;

        [SerializeField] private FloatDampener speedX;
        [SerializeField] private FloatDampener speedY;

        [SerializeField] private Camera camera;

        private Animator animator;
        Vector2 movInput;
        private int speedXHash;
        private int speedYHash;

        public void OnMove(InputAction.CallbackContext context) 
        {
            movInput = context.ReadValue<Vector2>();

            speedX.TargetValue = movInput.x;
            speedY.TargetValue = movInput.y;

            Vector3 floorNormal = transform.up;
            Vector3 camForward = camera.transform.forward;
            float angleInterpolator = Mathf.Abs(Vector3.Dot(camForward, floorNormal));

            Vector3 cameraForward = Vector3.Lerp(camForward, camera.transform.up, angleInterpolator).normalized;

            Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;

            Debug.DrawLine(transform.position, transform.position + characterForward * 2, Color.magenta, 5f);
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

            handleAnimations();
        }

        private void handleAnimations() 
        {
            animator.SetFloat(speedXHash, speedX.CurrentValue);
            animator.SetFloat(speedYHash, speedY.CurrentValue);
        }
    }
}
