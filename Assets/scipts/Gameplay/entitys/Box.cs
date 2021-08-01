using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IKilleable
{
    public System.Action OnBoxKill;
    public void kill()
    {
        OnBoxKill?.Invoke();
        Destroy(gameObject);
    }
}
