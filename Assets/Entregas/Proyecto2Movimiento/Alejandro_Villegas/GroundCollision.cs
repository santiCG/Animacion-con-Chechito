using UnityEngine;

public class GroundCollisionAV : MonoBehaviour
{
    struct RayResult
    {
        public Ray ray;
        public bool result;
        public RaycastHit hitInfo;
    }
    //[SerializeField] private Transform handIK;

    [SerializeField] private Transform legReference;
    [SerializeField] private float legLength;
    [SerializeField] private float profileThickness;


    [SerializeField] private LayerMask layerMask;

    RayResult rayResult;
    [SerializeField] private FloatDampener offset;
    private Animator anim;

    CharacterAVP character;

    private void Awake()
    {
        character = GetComponentInParent<CharacterAVP>();
        anim = GetComponent<Animator>();
    }
    private void SolveOffset()
    {
        RayResult result = new RayResult();
        result.ray = new Ray(legReference.position, legReference.forward);
        result.result = Physics.SphereCast(result.ray, profileThickness, out result.hitInfo, legLength, layerMask);
        rayResult = result;
        if (rayResult.result)
        {
            character.IsGrounded = true;
        }
        else
        {
            character.IsGrounded = false;
        }
    }
    private void FixedUpdate()
    {
        SolveOffset();
    }


#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        if (legReference == null) return;
        
        Vector3 startPos = legReference.position;
        Vector3 endPos = startPos + legReference.forward * legLength;
        Gizmos.DrawWireSphere(startPos, profileThickness);
        Gizmos.DrawWireSphere(endPos, profileThickness);
        Gizmos.DrawLine(startPos, endPos);


    }
#endif

}
