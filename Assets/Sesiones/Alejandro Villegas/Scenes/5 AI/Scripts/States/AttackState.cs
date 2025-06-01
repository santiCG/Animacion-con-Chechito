using UnityEngine;

public class AttackStateAV : IStateBehaviorAV<BasicEnemyContextAV>
{
    [SerializeField] private Vector2 waitTime;
    [SerializeField] private string attackTriggerName;

    private int attackTriggerId;

    private Animator animator;

    private float timer;
    private float currentTimer;
    public void OnEnter(BasicEnemyContextAV context)
    {
        attackTriggerId = Animator.StringToHash(attackTriggerName);
    }

    public void OnUpdate(BasicEnemyContextAV context)
    {

        if(timer > currentTimer)
        {
            animator.SetTrigger(attackTriggerId);
            timer = 0;
            currentTimer = Random.Range
        }


    }

}
