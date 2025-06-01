using UnityEngine;
using UnityEngine.AI;

public class PatrolStateAV : IStateBehaviorAV<BasicEnemyContextAV>
{

    [SerializeField] private float searchRadius = 10f;
    [SerializeField] private Vector2 searchTime = new Vector2(3f, 5f);
    
    private float timer;
    private float currentTime;
    public void OnEnter(BasicEnemyContextAV context)
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate(BasicEnemyContextAV context)
    {
        NavMeshAgent navigationAgent = context.agent.GetComponent<NavMeshAgent>();

        if(timer > currentTime)
        {
            Vector3 targetPosition = Vector3.ProjectOnPlane(Random.insideUnitSphere * Random.Range(0, searchRadius))

            navigationAgent.destination = targetPosition;
            timer = 0;
            currentTime = Random.Range(searchTime.x, searchTime.y);
        }

        if(navigationAgent.remainingDistance > navigationAgent.stoppingDistance * (1+0.1))
        {
            timer += Time.deltaTime;
        }
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
