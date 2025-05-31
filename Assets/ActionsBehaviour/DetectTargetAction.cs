using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetectTarget", story: "[Enemy] Detect [Target] In Range", category: "Action", id: "c4b2ad9ef585e03885c4a8bd4b2f8a1e")]
public partial class DetectTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private DetectPlayer detection;

    protected override Status OnStart()
    {
        detection = Enemy.Value.GetComponent<DetectPlayer>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(!detection.playerInRange) return Status.Running;
        
        GameObject target = detection.player;
        Target.Value = target;

        return Status.Success;
    }

    protected override void OnEnd()
    {

    }
}

