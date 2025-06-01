using UnityEngine;

public interface IDamageSenderAV <TDamage> where TDamage : struct
{
    void SendDamage(IDamageReceiverAV<TDamage> receiver);
    
}
