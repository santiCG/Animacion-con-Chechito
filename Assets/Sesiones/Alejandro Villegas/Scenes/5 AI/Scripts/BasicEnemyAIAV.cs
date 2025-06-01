using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAIAV : MonoBehaviour, IStateMachineAV<BasicEnemyContextAV>
{
    [SerializeField] public BasicEnemyContextAV context {  get; set; }

    public IStateBehaviorAV<BasicEnemyContextAV> currentState {  get; set; }

    private IStateBehaviorAV<BasicEnemyContextAV>[] states;

    private Dictionary<IStateBehaviorAV<BasicEnemyContextAV>, StateTransitionAV<BasicEnemyContextAV>[]> transitionsMap;
    
    public bool EvaluateTransitions()
    {
        foreach( StateTransitionAV<BasicEnemyContextAV> transition in transitionsMap[currentState])
        {
            if (transition.OnEvaluate(context)) {
                (IStateMachineAV<BasicEnemyContextAV>)this).
                return true;
            }
        }

        return false;
    }

    public void UpdateAI()
    {
        currentState.OnUpdate(context);
    }

    private void Awake()
    {
        //Construction code

        states = new[]
        {
           (IStateBehaviorAV<BasicEnemyContextAV>) new PatrolState(),
            new ChaseState(),
            new AttackState()
        };

        transitionsMap =
            new Dictionary<IStateBehaviorAV<BasicEnemyContextAV>, StateTransitionAV<BasicEnemyContextAV>[]>();
        transitionsMap.Add(states[0], new[]
        {
            new StateTransitionAV<BasicEnemyContextAV>(states[0], states[1], (_) => this.context.target != null)
        });
        transitionsMap.Add(states[1], new[]
        {
            new StateTransitionAV<BasicEnemyContextAV>(states[1], states[2], (_) => this.context.targetDistance < 1)
        });
        transitionsMap.Add(states[2], new[]
{
            new StateTransitionAV<BasicEnemyContextAV>(states[1], states[2], (_) => this.context.targetDistance > 10)
        });

        currentState = states[0];



        //new StateTransitionAV<BasicEnemyContextAV>(states[1], states[2], (_) => this.context.targetDistance < 1)
    }

    private void Update()
    {
        EvaluateTransitions();
        //Should update in longer intervals, consider implementing space and time partitioning
        UpdateAI();
    }
}
