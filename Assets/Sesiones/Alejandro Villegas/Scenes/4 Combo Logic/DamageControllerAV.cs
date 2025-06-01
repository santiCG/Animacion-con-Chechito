using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageControllerAV : MonoBehaviour
{
    private List<DamageMessageAV> damageList = new List<DamageMessageAV>();

    private bool ignoreDamage;
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EnqueueDamage(DamageMessageAV damage)
    {
        if (ignoreDamage || damageList.Any(dmg => dmg.sender == damage.sender)) return;
        damageList.Add(damage);
    }

    public void IFrameStart()
    {
        ignoreDamage = true;
    }

    public void IFrameEnd()
    {
        ignoreDamage = false;
    }

    private void Update()
    {
        Vector3 damageDirection = Vector3.zero;
        int damageLevel = 0;
        bool isDead = false;
        foreach (DamageMessageAV message in damageList) 
        {
            GameAV.Instance.PlayerOne.DepleteHealth(message.amount, out isDead); //Hardcoded to only one player
            damageDirection += (message.sender.transform.position - transform.position).normalized;
            damageLevel = Mathf.Max(damageLevel, (int)message.damageLevel);
        }

        if (damageList.Count == 0) return;
        damageDirection = Vector3.ProjectOnPlane(damageDirection.normalized, transform.up);
        float damageAngle = Vector3.SignedAngle(transform.forward, damageDirection, transform.up);
        animator.SetFloat("DamageDirection", (damageAngle / 180 * 0.5f + 0.5f));
        animator.SetInteger("DamageLevel", damageLevel);
        animator.SetTrigger("Damage");
        damageList.Clear();
        
        if (isDead )
        {

        }

    }
}
