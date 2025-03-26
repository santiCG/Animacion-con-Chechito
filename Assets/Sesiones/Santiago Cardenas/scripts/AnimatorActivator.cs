using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimatorActivator : MonoBehaviour
{
    private List<Action> activationEvent = new List<Action>();

    private Animator anim;

    protected virtual void InitializeEvents()
    {
        activationEvent.Add(() => 
        {
            anim.SetBool("canControl", true);
        });
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ActivateAnimator(int eventID)
    {
        activationEvent[eventID]?.Invoke();
    }
}
