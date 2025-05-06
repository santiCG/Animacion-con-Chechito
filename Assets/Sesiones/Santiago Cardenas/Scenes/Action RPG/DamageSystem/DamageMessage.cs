using System;
using UnityEngine;

[Serializable]
public struct DamageMessage
{
    public GameObject Sender;
    public float amount;
    public DamageLevel damageLevel;

    public enum DamageLevel
    {
        Small, 
        Medium,
        Big
    }

    public enum DamageType
    {
        Normal,
        Fire,
        Ice,
        Stun
    }
}
