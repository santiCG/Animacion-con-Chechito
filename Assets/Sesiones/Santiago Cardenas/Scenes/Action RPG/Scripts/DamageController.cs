using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private List<DamageMessage> damageList = new List<DamageMessage>();

    private bool ignoreDamage;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EnqueueDamage(DamageMessage damage)
    {
        if (ignoreDamage || damageList.Any(dmg => dmg.Sender == damage.Sender)) return;
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
        Vector3 damageDir = Vector3.zero;
        int damageLevel = 0;
        bool isDead = false;

        foreach (DamageMessage message in damageList)
        {
            Game.Instance.PlayerOne.DepleteHealth(message.amount, out bool dead); // esta mal, pq solo estamos accediendo al player1, en caso de que hubiera mas personajes solo modificariamos la vida del player1
            isDead = dead;
            damageDir += (message.Sender.transform.position - transform.position).normalized;
            damageLevel = Math.Max(damageLevel, (int)message.damageLevel);
        }

        if(damageList.Count > 0)
        {
            damageDir = Vector3.ProjectOnPlane(damageDir.normalized, transform.up);
            float damageAngle = Vector3.SignedAngle(transform.forward, damageDir, transform.up);

            animator.SetFloat("DamageDir", (damageAngle/180) * 0.5f + 0.5f);
            animator.SetInteger("DamageIntensity", damageLevel);
            animator.SetTrigger("Hit");

            if(isDead)
            {
                animator.SetTrigger("Die");
            }
            damageList.Clear();
        }
    }
}
