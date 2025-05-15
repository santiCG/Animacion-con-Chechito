using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IStateBehavior<BasicEnemyAIContext>
{
    public void OnEnter(BasicEnemyAIContext context)
    {
        
    }

    public void OnUpdate(BasicEnemyAIContext context)
    {
        if (context.target == null)
        {
            context.targetDistance = Vector3.Distance(context.agent.transform.position, context.player.transform.position);
            return;
        }

        NavMeshAgent navAgent = context.agent.GetComponent<NavMeshAgent>();
        navAgent.destination = context.target.position;
        context.targetDistance = navAgent.remainingDistance;
    }
}
