using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFx : MonoBehaviour
{
    [SerializeField]GameObject explotion;
    [SerializeField] GameObject canonPoint;
    void Spawn_ShootEffect_1()
    {
        Instantiate(explotion,canonPoint.transform.position,Quaternion.identity,null);
    }
}
