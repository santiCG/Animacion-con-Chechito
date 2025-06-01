using UnityEngine;

public class DamageReceiverAV : MonoBehaviour , IDamageReceiverAV<float>
{
    public void ReceiveDamage(float damage)
    {
        //Reducir vida del personaje
        //Accionar muerte si la vida es baja
        Debug.Log("Muelto");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
