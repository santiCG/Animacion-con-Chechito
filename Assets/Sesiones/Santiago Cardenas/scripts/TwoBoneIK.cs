using UnityEngine;

public class TwoBoneIK : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float targetWeight;
    [SerializeField][Range(0, 1)] private float hintWeight;

    [SerializeField] private AvatarIKGoal ikGoal;
    [SerializeField] private AvatarIKHint ikHint;

    [SerializeField] private Transform ikTarget;
    [SerializeField] private Transform hintTarget;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(ikGoal, targetWeight * (ikTarget == null ? 0 : 1));
        anim.SetIKRotationWeight(ikGoal, targetWeight * (ikTarget == null ? 0 : 1));
        anim.SetIKPosition(ikGoal, ikTarget.position);
        anim.SetIKRotation(ikGoal, ikTarget.rotation);

        float hWeight = hintWeight * (hintTarget == null ? 0 : 1);
        anim.SetIKHintPositionWeight(ikHint, hWeight);
        anim.SetIKHintPosition(ikHint, hintTarget.position);
    }
}
