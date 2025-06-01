using UnityEngine;

public class HitboxAV : MonoBehaviour, IDamageSenderAV<DamageMessageAV>
{
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageReceiverAV<DamageMessageAV> receiver))
        {
            SendDamage(receiver);
        }
    }

    public void SendDamage(IDamageReceiverAV<DamageMessageAV> receiver)
    {
        DamageMessageAV message = new DamageMessageAV()
        {
            sender = this.gameObject,
            amount = damage
        };
        receiver.ReceiveDamage(message);
    }
}
