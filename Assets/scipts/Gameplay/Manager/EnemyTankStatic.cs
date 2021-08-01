using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankStatic : Tank
{
    [Header("EnemyTank")]
    [SerializeField] PlayerFx playerFx;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float distanceToShoot;
    Transform toFolow;

    void Start()
    {
        playerFx.OnShoot += Shoot;
        OnStartShoot += StartShoot;
    }
    void StartShoot()
    {
        animator.Play("shoot");
    }
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distanceToShoot, playerLayer);
        foreach (Collider hit in colliders)
        {
            if (!CR_running)
            {
                Target = hit.transform.position;
                StartCoroutine(headRotate(Target));
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToShoot);
    }
}

