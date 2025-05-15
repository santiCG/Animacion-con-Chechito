using Unity.VisualScripting;
using UnityEngine;

public class StateTransitions<TContext>
{
    public delegate bool StateTransitionsDelegate(TContext context);

    public IStateBehavior<TContext> from;
    public IStateBehavior<TContext> to;

    public StateTransitionsDelegate onEvaluate;

    public StateTransition(IStateBehavior<TContext> from,IStateBehavior<TContext> to, StateTransitionsDelegate onEvaluate)
    {
        this.from = from; 
        this.to = to; 
        this.onEvaluate = onEvaluate;
    }
}
