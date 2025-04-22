using UnityEngine;

public struct DamageMessage
{
    public GameObject Sender;
    public float amount;

    public enum DamageType
    {
        Normal,
        Fire,
        Ice,
        Stun
    }
}
