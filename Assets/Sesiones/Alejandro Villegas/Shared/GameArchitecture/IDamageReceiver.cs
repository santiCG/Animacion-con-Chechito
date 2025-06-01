using UnityEngine;

public interface IDamageReceiverAV <TDamage> where TDamage : struct //Definicion generica c# c++ template
{
    void ReceiveDamage(TDamage damage);
}
