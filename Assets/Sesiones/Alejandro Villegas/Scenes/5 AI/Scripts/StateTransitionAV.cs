using UnityEngine;


public class StateTransitionAV<TContext> where TContext : class
{
    public delegate bool StateTransitionDelegate(TContext context);

    private IStateBehaviorAV<TContext> from;
    private IStateBehaviorAV<TContext> to;

    public StateTransitionDelegate OnEvaluate;

    public StateTransitionAV(IStateBehaviorAV<TContext> from, IStateBehaviorAV<TContext> to,StateTransitionDelegate onEvaluate)
    {
        this.from = from;
        this.to = to;
        this.OnEvaluate = onEvaluate;
    }

}
