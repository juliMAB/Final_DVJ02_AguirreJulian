using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFx : MonoBehaviour
{
    [SerializeField] GameObject WaterSpash;
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = Instantiate(WaterSpash, other.ClosestPoint(transform.position), Quaternion.identity, null);
        go.name = "waterSplash";
        Destroy(go, 1.5f);
    }
}
