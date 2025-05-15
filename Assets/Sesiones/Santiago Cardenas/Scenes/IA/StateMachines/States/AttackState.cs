using UnityEngine;

public class AttackState : IStateBehavior<BasicEnemyAIContext>
{
    [SerializeField] private Vector2 waitTime;
    [SerializeField] private string attackTriggerName;

    private int attackTriggerID;
    private float timer;
    private float currentTime;

    private Animator animator;

    public void OnEnter(BasicEnemyAIContext context)
    {
        attackTriggerID = Animator.StringToHash(attackTriggerName);
        animator = context.agent.GetComponent<Animator>();
    }

    public void OnUpdate(BasicEnemyAIContext context)
    {
        if (timer > currentTime)
        {
            animator.SetTrigger(attackTriggerID);
            timer = 0f;
            currentTime = Random.Range(waitTime.x, waitTime.y);
        }

        timer += Time.deltaTime;

        context.targetDistance = Vector3.Distance(context.agent.transform.position, context.player.transform.position);
    }
}
