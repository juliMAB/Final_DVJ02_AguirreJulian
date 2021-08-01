using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : Tank
{
    [Header("EnemyTank")]
    [SerializeField] PlayerFx playerFx;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float distanceToFollow;
    [SerializeField] float distanceToShoot;
    Transform toFolow;

    void Start()
    {
        playerFx.OnShoot += Shoot;
    }
    
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distanceToFollow, playerLayer);
        foreach (Collider hit in colliders)
        {
            toFolow = hit.transform;
        }
        if (toFolow != null)
        {
            float distance = Vector3.Distance(toFolow.position, transform.position);
            if (distance <= distanceToFollow)
            {
                animator.SetBool("OnMove", true);
                if (Vector3.Distance(transform.position, toFolow.position) <= distanceToShoot)
                {
                    animator.SetBool("OnMove", false);

                    if (!CR_running)
                    {
                        Target = toFolow.position;
                        animator.Play("shoot");
                    }

                }
                else
                {
                    animator.SetBool("OnMove", true);
                }
                FaceTarget(toFolow.position);
                agent.SetDestination(toFolow.position);
            }
            else
            {
                animator.SetBool("OnMove", false);
            }
        }
        
    }

        void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToFollow);
    }
}

