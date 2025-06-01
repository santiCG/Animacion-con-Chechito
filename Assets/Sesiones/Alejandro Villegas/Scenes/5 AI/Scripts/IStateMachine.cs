using UnityEngine;

public interface IStateMachineAV<TContext> where TContext : class
{
    public TContext context { get; set; }
    public IStateBehaviorAV<TContext> currentState { get; set; }

    void SwitchState(IStateBehaviorAV<TContext> nextState)
    {
        currentState = nextState;
        nextState.OnEnter(context);
    }

    bool EvaluateTransitions();

}
