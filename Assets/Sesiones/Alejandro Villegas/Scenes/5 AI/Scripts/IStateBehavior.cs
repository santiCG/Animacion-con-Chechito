using UnityEngine;

public interface IStateBehaviorAV <TContext > where TContext : class
{
    void OnEnter(TContext context);
    void OnUpdate(TContext context);
}
