using System.Runtime.InteropServices;
using UnityEngine;

public class WeaponCollisionAdjust : MonoBehaviour
{
    struct RayResult
    {
        public Ray ray;
        public bool result;
        public RaycastHit hitInfo;
    }

    [SerializeField] private AvatarIKGoal triggerHand;
    [SerializeField] private AvatarIKGoal holdingHand;
    [SerializeField] private Transform weaponRef;
    [SerializeField] private Transform weaponHandle;
    [SerializeField] private float weaponLegth;
    [SerializeField] private float weaponThickness;

    [SerializeField] private LayerMask layer;
    Animator animator;

    RayResult rayResult;
    [SerializeField] private FloatDampener offset;

    Character character;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        SolveOffset();
    }

    private void SolveOffset()
    {
        RayResult result = new RayResult();
        Ray r = new Ray(weaponRef.position, weaponRef.forward);
        result.ray = r;
        result.result = Physics.SphereCast(result.ray, weaponThickness, out result.hitInfo, weaponLegth, layer);
        rayResult = result;

        offset.TargetValue = Mathf.Max(0, Mathf.Abs(weaponLegth) - Vector3.Distance(rayResult.hitInfo.point, weaponRef.position)) * -1f;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(character.isAiming)
        {
            offset.Update();

            Vector3 originalIKpos = animator.GetIKPosition(triggerHand);
            animator.SetIKPositionWeight(triggerHand, 1);
            animator.SetIKPosition(triggerHand, originalIKpos + transform.forward * offset.CurrentValue);

            animator.SetIKPositionWeight(holdingHand, 1);
            animator.SetIKPosition(holdingHand, weaponHandle.position);
        }
        else
        {
            animator.SetIKPositionWeight(holdingHand, 0);
            animator.SetIKPositionWeight(triggerHand, 0);
        }

    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if(weaponRef == null) return;

        Gizmos.color = rayResult.result ? Color.green : Color.red;
        Vector3 startPos = weaponRef.position;
        Vector3 endPos = startPos + weaponRef.forward * weaponLegth;
        Gizmos.DrawWireSphere(startPos, weaponThickness);
        Gizmos.DrawWireSphere(endPos, weaponThickness);
        Gizmos.DrawLine(startPos, endPos);
    }
#endif
}
