using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject goExplotion;
    [SerializeField] float power = 10.0f;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float upforce = 1.0f;
    [SerializeField] int damage = 10;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Animator animator;

    public enum EnemyState
    {
        Idle, //0
        ChargeExplotion, //1
        Deathing, //2
        Last,
    }
    private EnemyState state = EnemyState.Idle;
    [SerializeField] float distanceToActive = 7;
    [SerializeField] float timeToExplote = 5;


    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Collider[] colliders = Physics.OverlapSphere(transform.position, distanceToActive, playerLayer);
                foreach (Collider hit in colliders)
                {
                    Debug.Log("enteraste");
                    animator.SetTrigger("NextState");
                    NextState();
                }
                break;
            case EnemyState.ChargeExplotion:
                timeToExplote -= Time.deltaTime;

                if (timeToExplote < 0)
                {
                    animator.SetTrigger("NextState");
                    NextState();
                }

                break;
            case EnemyState.Deathing:
                Detonate();

                break;
        }
    }
    void Detonate()
    {
        GameObject go;
        go = Instantiate(goExplotion, transform.position, Quaternion.identity, null);
        Destroy(go, 1.5f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, transform.position, radius, upforce, ForceMode.Impulse); 
            }
            IHitable hitable = hit.gameObject.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
    void NextState()
    {
        switch (state)
        {
            case EnemyState.Idle:
                state = EnemyState.ChargeExplotion;
                break;
            case EnemyState.ChargeExplotion:
                state = EnemyState.Deathing;
                break;
            case EnemyState.Deathing:
                state = EnemyState.Last;
                break;
            case EnemyState.Last:
                
                break;
            default:
                break;
        }
    }
}
