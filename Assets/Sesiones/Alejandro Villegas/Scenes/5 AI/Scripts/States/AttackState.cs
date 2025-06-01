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
        animator = context.agent.GetComponent<Animator>();
    }

    public void OnUpdate(BasicEnemyContextAV context)
    {

        if(timer > currentTimer)
        {
            animator.SetTrigger(attackTriggerId);
            timer = 0;
            currentTimer = Random.Range(waitTime.x, waitTime.y);
        }

        timer += Time.deltaTime;
        context.targetDistance = Vector3.Distance(context.agent.transform.position, context.player.transform.position);



    }

}
