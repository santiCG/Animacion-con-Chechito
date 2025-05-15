using System;
using UnityEngine;

[Serializable]
public class BasicEnemyAIContext : MonoBehaviour
{
    public GameObject agent;
    public GameObject player;
    public Transform target;
    public float targetDistance;
}
