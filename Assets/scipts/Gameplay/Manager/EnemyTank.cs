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
    [SerializeField] LayerMask player;
    [SerializeField] float distanceToFollow;
    [SerializeField] float distanceToShoot;
    Transform target;

    void Start()
    {
        playerFx.OnShoot += Shoot;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((player & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            target = other.transform;
        }
    }
    void Update()
    {
        //if (Physics.SphereCast(transform.position, 10, transform.forward, out RaycastHit hit, 10))
        //{
        //    Debug.Log("Te veo perra");
        //    agent.SetDestination(hit.point);
        //    animator.SetBool("OnMove", true);
        //    if (Vector3.Distance(transform.position, hit.point) <= distanceToShoot)
        //    {
        //        animator.SetBool("OnMove", false);

        //        if (!CR_running)
        //        {
        //            Target = hit.point;
        //            animator.Play("shoot");
        //        }

        //    }
        //    else
        //    {
        //        animator.SetBool("OnMove", true);
        //    }
        //    FaceTarget(hit.point);
        //}
        //else
        //{
        //    animator.SetBool("OnMove", false);
        //}
        if (target!=null)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= distanceToFollow)
            {
                animator.SetBool("OnMove", true);
                if (Vector3.Distance(transform.position, target.position) <= distanceToShoot)
                {
                    animator.SetBool("OnMove", false);

                    if (!CR_running)
                    {
                        Target = target.position;
                        animator.Play("shoot");
                    }

                }
                else
                {
                    animator.SetBool("OnMove", true);
                }
                FaceTarget(target.position);
                agent.SetDestination(target.position);
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

