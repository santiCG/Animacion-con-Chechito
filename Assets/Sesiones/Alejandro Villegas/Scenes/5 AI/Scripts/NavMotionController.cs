using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterMovement2AV))]
public class NavMotionController : MonoBehaviour
{
    NavMeshAgent agent;
    CharacterMovement2AV characterMovement;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
        characterMovement = GetComponent<CharacterMovement2AV>();
    }

    private void SolveMotion()
    {
        Vector3 navigationDelta = agent.nextPosition - transform.position;

        float deltaX = Vector3.Dot(transform.right, navigationDelta);
        float deltaY = Vector3.Dot(transform.forward, navigationDelta);

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            deltaX = 0; 
            deltaY = 0;
        }

        characterMovement.SetMotionVector(deltaX * 1.5f, deltaY * 1.5f);
    }

    private void Update()
    {
        SolveMotion();
    }
}
