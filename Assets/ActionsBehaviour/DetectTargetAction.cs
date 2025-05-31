using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Detect Target", story: "[Enemy] [Detect] [Target]", category: "Action", id: "5fb0f0309d9ea8d38aac52b9d1d84e40")]
public partial class DetectTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<DetectPlayer> Detect;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        Target.Value = Detect.Value.ReturnPlayerObj();

        return Target.Value == null ? Status.Failure : Status.Success;
    }
}

