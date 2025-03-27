using UnityEngine;

public class TwoBoneIKAV : MonoBehaviour
{

    [SerializeField] private AvatarIKGoal iKGoal;
    [SerializeField] private AvatarIKHint iKHint;


    [SerializeField] private Transform iKTarget;
    [SerializeField] private Transform hintTarget;

    [SerializeField][Range(0, 1)] private float targetWeight;
    [SerializeField][Range(0, 1)] private float hintWeight;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float tWeight = targetWeight * (iKTarget == null ? 0 : 1);
        animator.SetIKPositionWeight(iKGoal, tWeight);
        animator.SetIKRotationWeight(iKGoal, tWeight);
        animator.SetIKPosition(iKGoal, iKTarget.position);
        animator.SetIKRotation(iKGoal, iKTarget.rotation);

        float hWeight = hintWeight * (hintTarget == null ? 0 : 1);
        animator.SetIKHintPositionWeight(iKHint, hWeight);
        animator.SetIKHintPosition(iKHint, hintTarget.position);


    }
}
