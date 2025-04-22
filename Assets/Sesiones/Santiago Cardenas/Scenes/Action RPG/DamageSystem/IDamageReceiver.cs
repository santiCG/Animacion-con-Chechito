using UnityEngine;

public interface IDamageReceiver<in TDamage> where TDamage : struct
{
    void ReceiveDamage(TDamage damage);
}
