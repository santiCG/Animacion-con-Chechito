using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimatorActivatorAV : MonoBehaviour
{
    private List <Action> activationEvents = new List<Action>();
    private Animator anim;
    protected virtual void InitializeEvents()
    {
        activationEvents.Add( () =>
        {
            anim.SetBool("CanControl", true);
        }
            );
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        InitializeEvents();
    }

    public void ActivateAnimator(int eventID)
    {
        activationEvents[eventID]?.Invoke();
    }
}
