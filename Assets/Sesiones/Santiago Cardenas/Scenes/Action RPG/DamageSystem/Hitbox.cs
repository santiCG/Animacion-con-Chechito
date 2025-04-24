using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageSender<DamageMessage>
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageReceiver<DamageMessage> receiver))
        {
            SendDamage(receiver);
        }
    }

    public void SendDamage(IDamageReceiver<DamageMessage> receiver)
    {
        DamageMessage damageMessage = new DamageMessage
        {
            Sender = transform.root.gameObject,
            amount = damage
        };

        Debug.Log($"Enviador de Dano: {name}");

        receiver.ReceiveDamage(damageMessage);
    }
}
