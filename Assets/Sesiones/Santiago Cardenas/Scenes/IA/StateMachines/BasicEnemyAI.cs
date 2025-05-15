using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IStateMachine<BasicEnemyAIContext>
{
    public BasicEnemyAIContext context { get; set; }

    public IStateBehavior<BasicEnemyAIContext> currentState { get; set; }

    private IStateBehavior<BasicEnemyAIContext>[] states;

    private Dictionary<IStateBehavior<BasicEnemyAIContext>, StateTransitions<BasicEnemyAIContext>[]> transitionMap;

    private void Awake()
    {
        states = new[]
        {
            (IStateBehavior<BasicEnemyAIContext>)new PatrolState(),
            new ChaseState(),
            new AttackState()
        };

        transitionMap = new Dictionary<IStateBehavior<BasicEnemyAIContext>, StateTransitions<BasicEnemyAIContext>[]>();

        transitionMap.Add(states[0], new[]
        {
            new StateTransitions<BasicEnemyAIContext>(states[0], states[1], (_) => this.context.target != null)
        });

        transitionMap.Add(states[1], new[]
        {
            new StateTransitions<BasicEnemyAIContext>(states[1], states[2], (_) => this.context.targetDistance < 1)
        });

        transitionMap.Add(states[2], new[]
        {
            new StateTransitions<BasicEnemyAIContext>(states[2], states[0], (_) => this.context.targetDistance > 10)
        });
    }

    public bool EvaluateTransitions()
    {
        foreach(StateTransitions<BasicEnemyAIContext> trasition in transitionMap[currentState])
        {
            if (trasition.onEvaluate(context)) 
            {
                ((IStateMachine<BasicEnemyAIContext>)this).SwitchState(trasition.to);
                return true; 
            }
        }

        return false;
    }

    private void UpdateAI()
    {
        currentState.OnUpdate(context);
    }

    private void Start()
    {
        InvokeRepeating("UpdateAI", 0f, 1f);
    }
}
