using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] GameObject explotionTarget;
    [SerializeField] int damage=5;
    void Start()
    {
        Destroy(gameObject,5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IKilleable killeable = collision.gameObject.GetComponent<IKilleable>();
        if (killeable!=null)
        {
            killeable.kill();
        }
        IHitable hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable!=null)
        {
            hitable.TakeDamage(damage);
        }
        GameObject go = Instantiate(explotionTarget, collision.contacts[0].point, Quaternion.identity, null);
        Destroy(go, 2);
        Destroy(gameObject);
    }
}
