using UnityEngine;
using UnityEngine.AI;


public class ChaseStateAV : IStateBehaviorAV<BasicEnemyContextAV>
{
    public void OnEnter(BasicEnemyContextAV context)
    {

    }

    public void OnUpdate(BasicEnemyContextAV context)
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