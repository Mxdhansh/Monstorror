using UnityEngine;
using UnityEngine.AI;

public class NPCWander : MonoBehaviour
{
    public Transform monster;
    public float detectionRange = 8f;
    public float wanderRadius = 15f;
    public float walkSpeed = 2f;
    public float runSpeed = 5.5f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        timer = waitTime;
    }

    void Update()
    {
        if (monster != null)
        {
            float distance = Vector3.Distance(transform.position, monster.position);

            if (distance < detectionRange)
            {
                agent.speed = runSpeed;

                Vector3 runDirection = (transform.position - monster.position).normalized;
                Vector3 destination = transform.position + runDirection * wanderRadius;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(destination, out hit, wanderRadius, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }

                return;
            }
        }

        agent.speed = walkSpeed;

        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            timer = 0f;
        }
    }
}