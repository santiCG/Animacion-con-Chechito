using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent(typeof(CharactercitoMovement))]
public class EsothericNavigation : MonoBehaviour
{
    NavMeshAgent agent;
    CharactercitoMovement movement;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<CharactercitoMovement>();
        agent.updatePosition = true;
        agent.updateRotation = true;
    }

    private void SolveMotion()
    {
        Vector3 navDelta = agent.nextPosition - transform.position;

        float deltaX = Vector3.Dot(transform.right, navDelta);
        float deltaY = Vector3.Dot(transform.forward, navDelta);

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            deltaX = 0f;
            deltaY = 0f;
        }

        movement.SetMotionVector(deltaX * 1.5f, deltaY * 1.5f);
    }

    private void Update()
    {
        SolveMotion();
    }
}
