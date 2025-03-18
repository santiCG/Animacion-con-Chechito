using UnityEngine;

public class WeaponCollisionAdjust : MonoBehaviour
{
    struct RayResult
    {
        public Ray ray;
        public bool result;
        public RaycastHit hitInfo;
    }


    [SerializeField] private Transform weaponRef;
    [SerializeField] private float weaponLegth;
    [SerializeField] private float weaponThickness;

    [SerializeField] private LayerMask layer;

    RayResult rayResult;
    private float offset;

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

        offset = Mathf.Max(0, weaponLegth - Vector3.Distance(rayResult.hitInfo.point, weaponRef.position)) * -1f;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(weaponRef == null) return;

        Vector3 startPos = weaponRef.position;
        Vector3 endPos = startPos - weaponRef.forward * weaponLegth;
        Gizmos.DrawWireSphere(startPos, weaponThickness);
        Gizmos.DrawWireSphere(endPos, weaponThickness);
        Gizmos.DrawLine(startPos, endPos);
    }
#endif
}
