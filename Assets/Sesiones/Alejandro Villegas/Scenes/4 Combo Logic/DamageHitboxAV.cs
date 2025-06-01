using UnityEngine;
using System;
using UnityEngine.Events;

public class DamageHitboxAV : MonoBehaviour, IDamageReceiverAV<DamageMessageAV>
{
    [Serializable]
    public class AttackQueueEvent : UnityEvent<DamageMessageAV>
    {

    }

    [SerializeField] private float defenseMultiplier = 1f;
    public AttackQueueEvent OnHit;

    public void ReceiveDamage(DamageMessageAV damage)
    {

        if(damage.sender == transform.root.gameObject)
        {
            return;
        }
        damage.amount = damage.amount * defenseMultiplier;
        OnHit?.Invoke(damage);
        Debug.Log($"received damage ({damage.amount})");
    }
}
