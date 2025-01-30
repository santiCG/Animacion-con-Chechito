using UnityEditor.Animations;
using UnityEngine;

namespace Sesiones.Santiago.Animation
{
    [RequireComponent(typeof(Animator))]

    public class AnimatorControls : MonoBehaviour
    {
        [SerializeField][Range(-1f, 1f)] private float speedX;
        [SerializeField][Range(-1f, 1f)] private float speedY;

        private Animator animator;

        private int speedXHash;
        private int speedYHash;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            speedXHash = Animator.StringToHash("SpeedX");
            speedYHash = Animator.StringToHash("SpeedY");
        }
        private void Update()
        {
            animator.SetFloat("SpeedX", speedX);
            animator.SetFloat("SpeedY", speedY);
        }
    }
}
