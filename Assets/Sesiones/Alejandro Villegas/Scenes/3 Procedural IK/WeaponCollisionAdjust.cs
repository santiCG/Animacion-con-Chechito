using UnityEngine;

public class WeaponCollisionAdjustAV : MonoBehaviour
{
    struct RayResult
    {
        public Ray ray;
        public bool result;
        public RaycastHit hitInfo;
    }
    [SerializeField] AvatarIKGoal triggerHand = AvatarIKGoal.RightHand;
    [SerializeField] AvatarIKGoal holdHand = AvatarIKGoal.LeftHand;
    //[SerializeField] private Transform handIK;

    [SerializeField] private Transform weaponReference;
    [SerializeField] private Transform weaponHandle;
    [SerializeField] private float weaponLength;
    [SerializeField] private float profileThickness;


    [SerializeField] private LayerMask layerMask;

    RayResult rayResult;
    [SerializeField] private FloatDampener offset;
    private Animator anim;

    CharacterAV character;

    private void Awake()
    {
        character = GetComponent<CharacterAV>();
        anim = GetComponent<Animator>();
    }
    private void SolveOffset()
    {
        RayResult result = new RayResult();
        result.ray = new Ray(weaponReference.position, weaponReference.forward);
        result.result = Physics.SphereCast(result.ray, profileThickness, out result.hitInfo, weaponLength, layerMask);
        rayResult = result;
        offset.TargetValue = Mathf.Max(0, weaponLength - Vector3.Distance(rayResult.hitInfo.point, weaponReference.position)) * -1f;
    }
    private void FixedUpdate()
    {
        SolveOffset();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        offset.Update();

        if (character.IsAiming)
        {
            Vector3 originalIKPosition = anim.GetIKPosition(triggerHand);
            anim.SetIKPositionWeight(triggerHand, 1);
            anim.SetIKPosition(triggerHand, originalIKPosition + weaponReference.forward * offset.CurrentValue);
            anim.SetIKPositionWeight(holdHand, 1);
            anim.SetIKPosition(holdHand, weaponHandle.position);
        }
        else
        {
            anim.SetIKPositionWeight(triggerHand, 0);
            anim.SetIKPositionWeight(holdHand, 0);
        }



    }

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        if (weaponReference == null) return;
        
        Vector3 startPos = weaponReference.position;
        Vector3 endPos = startPos + weaponReference.forward * weaponLength;
        Gizmos.DrawWireSphere(startPos, profileThickness);
        Gizmos.DrawWireSphere(endPos, profileThickness);
        Gizmos.DrawLine(startPos, endPos);


    }
#endif

}
