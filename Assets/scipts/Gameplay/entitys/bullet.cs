using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] GameObject explotionTarget;
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
        GameObject go = Instantiate(explotionTarget, collision.contacts[0].point, Quaternion.identity, null);
        Destroy(go, 2);
        Destroy(gameObject);
    }
}
