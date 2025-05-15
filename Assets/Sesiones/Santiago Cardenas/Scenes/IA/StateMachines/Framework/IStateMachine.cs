using UnityEngine;

public interface IStateMachine<TContext> where TContext : class
{
    TContext context { get; }
    IStateBehavior<TContext> currentState { get; set; }
    void SwitchState(IStateBehavior<TContext> nextState)
    {
        currentState = nextState;
        nextState.OnEnter(context);
    }

    bool EvaluateTransitions();
}
