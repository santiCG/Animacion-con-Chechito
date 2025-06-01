using UnityEngine;

public class HitboxAV : MonoBehaviour, IDamageSenderAV<DamageMessageAV>
{
    [SerializeField] private DamageMessageAV damage;
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
            sender = transform.root.gameObject,
            amount = damage.amount,
            damageLevel = damage.damageLevel
        };
        receiver.ReceiveDamage(message);
    }
}
