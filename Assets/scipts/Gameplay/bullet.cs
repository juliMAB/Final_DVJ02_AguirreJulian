using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
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
        Destroy(gameObject);
    }
}
