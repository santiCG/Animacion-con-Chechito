using Unity.Behavior;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool playerInRange;
    public GameObject player;

    [SerializeField] private float detectionRadius;

    private BehaviorGraphAgent bagent;

    private void Awake()
    {
        bagent = GetComponent<BehaviorGraphAgent>();
    }

    public GameObject ReturnPlayerObj()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (var item in detectedObjects)
        {
            if (item.CompareTag("Player"))
            {
                player = item.gameObject;
                bagent.SetVariableValue("TargetDetected", true);
                return player;
            }
        }

        bagent.SetVariableValue("TargetDetected", false);
        player = null;
        return player;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
}
