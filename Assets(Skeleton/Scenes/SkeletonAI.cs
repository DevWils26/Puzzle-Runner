using UnityEngine;
using UnityEngine.AI;


public class SkeletonAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;


    public float chaseRange = 10f;
    public float attackRange = 1.5f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent.stoppingDistance = attackRange;
    }




    void Update()
    {
        if (player == null) return;


        float distance = Vector3.Distance(transform.position, player.position);


        if (distance <= attackRange)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);


            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("Attack");
            }


            return;
        }


        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            agent.ResetPath();
            animator.SetFloat("Speed", 0f);
        }
    }
}
