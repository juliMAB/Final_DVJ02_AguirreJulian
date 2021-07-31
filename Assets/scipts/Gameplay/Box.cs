using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IKilleable
{
    public void kill()
    {
        Destroy(gameObject);
    }
}
