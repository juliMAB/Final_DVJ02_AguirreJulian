using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerFx : MonoBehaviour
{
    public Action OnShoot;
    [SerializeField]GameObject explotion;
    [SerializeField] GameObject canonPoint;
    void Spawn_ShootEffect_1()
    {
        OnShoot?.Invoke();
        GameObject go = Instantiate(explotion,canonPoint.transform.position,Quaternion.identity,null);
        Destroy(go, 2);
    }
    void Destroy_TankEffect_1()
    {
        Destroy(gameObject);
    }
}
