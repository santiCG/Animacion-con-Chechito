using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IStateBehavior<BasicEnemyAIContext>
{
    [SerializeField] private float searchRadius;
    [SerializeField] private Vector2 searchTime;

    private float timer;
    private float currentTime;

    public void OnEnter(BasicEnemyAIContext context)
    {
        Debug.Log("Patrol");
    }

    public void OnUpdate(BasicEnemyAIContext context)
    {
        NavMeshAgent navAgent = context.agent.GetComponent<NavMeshAgent>();
        if (timer > currentTime)
        {
            Vector3 targetPos = Vector3.ProjectOnPlane(Random.insideUnitSphere * Random.Range(0, searchRadius), context.agent.transform.up);
            navAgent.destination = targetPos;
            timer = 0;
            currentTime = Random.Range(searchTime.x, searchTime.y);
        }

        if(navAgent.remainingDistance > navAgent.stoppingDistance * (1 + 0.01f))
        {
            timer += Time.deltaTime;
        }

        context.targetDistance = Vector3.Distance(navAgent.transform.position, context.player.transform.position);
    }
}
