using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageHitbox : MonoBehaviour, IDamageReceiver<DamageMessage>
{
    [Serializable]
    public class AttackHitEvent : UnityEvent<DamageMessage>
    {

    }

    [SerializeField] private float defenseMultiplier = 0.9f;

    public AttackHitEvent OnHit;

    public void ReceiveDamage(DamageMessage damage)
    {
        // Verificar si es el propio jugador el que se esta haciendo daño a si mismo
        if(damage.Sender == transform.root.gameObject)
        {
            return;
        }

        damage.amount *= defenseMultiplier;

        // Quitar vida
        OnHit?.Invoke(damage);
        Debug.Log($"Dano recibido: {damage.amount}");
    }
}
