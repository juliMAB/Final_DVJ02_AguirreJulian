using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBombTank : Tank
{
    public Transform player;
    [SerializeField] float speed;
    [SerializeField] LayerMask playerLayer;
    NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } set { agent = value; } }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    void Update()
    {
        FaceTarget(player.position);
        agent.SetDestination(player.position);
    }
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IHitable hitable = collision.gameObject.GetComponent<IHitable>();
        if (collision.gameObject.transform == player && hitable != null)
        {
            death();
            hitable.TakeDamage(1);
        }
    }
}
