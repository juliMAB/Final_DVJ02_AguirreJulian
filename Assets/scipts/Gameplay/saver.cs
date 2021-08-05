using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saver : MonoBehaviour
{
    [SerializeField]LayerMask layerMask;
    public System.Action OnPlayerCollider;
    private void OnCollisionEnter(Collision collision)
    {
        if ((layerMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            OnPlayerCollider?.Invoke();
        }
    }
}
