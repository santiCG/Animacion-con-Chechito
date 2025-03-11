using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CharacterShared : MonoBehaviour
{
    [SerializeField] Transform lockTarget;

    public Transform LockTarget
    {
        get => lockTarget;
        set => lockTarget = value;
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponentShared characterComponent in GetComponentsInChildren<ICharacterComponentShared>())
        {
            characterComponent.ParentCharacter = this;
        }
    }

    private void Awake()
    {
        RegisterComponents();
    }
}
