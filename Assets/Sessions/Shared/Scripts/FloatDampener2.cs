using System;
using UnityEngine;


[Serializable]
public struct FloatDampener2
{
    [SerializeField] private float smoothTime;
    
    private float currentVelocity;

    public float TargetValue { get; set; }
    public float CurrentValue { get; private set; }


    public void Update()
    {
        CurrentValue = Mathf.SmoothDamp(CurrentValue, TargetValue, ref currentVelocity, smoothTime);
    }
}
