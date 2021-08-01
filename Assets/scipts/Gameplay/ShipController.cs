﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float totalDistance;
    public Action OnStartShoot;
    bool moving;
    public Action<bool> OnStartMove;
    public float TotalDistance => totalDistance;
    [SerializeField] float velocity;
    [SerializeField] float angleVelocity;
    Rigidbody rb;
    [SerializeField] GameObject pivCanon;
    [SerializeField] GameObject pivHead;
    [SerializeField] GameObject canonPoint;
    [SerializeField] GameObject goShoot;
    [SerializeField] float shootForce;
    [Range(2,0)]
    [SerializeField] float rotationSpeed;
    Vector3 target;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100);
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.W))
        {
            if (moving)
            {
                moving = false;
                OnStartMove?.Invoke(moving);
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.up * Time.deltaTime * -angleVelocity);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up * Time.deltaTime * angleVelocity);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Vector3 res = transform.forward * Time.deltaTime * velocity;
            float distance = Mathf.Abs(Vector3.Distance(transform.position, transform.position + res));
            totalDistance += distance;
            transform.position += (res);
            moving = true;
            OnStartMove?.Invoke(moving) ;
           
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Vector3 res = transform.forward * Time.deltaTime * velocity;
            float distance = Mathf.Abs(Vector3.Distance(transform.position, transform.position + res));
            totalDistance += distance;
            transform.position -= (res);
            moving = true;
            OnStartMove?.Invoke(moving);
        }
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                StopAllCoroutines();
                IKilleable killeable = hit.collider.gameObject.GetComponent<IKilleable>();
                if (killeable != null)
                {
                    StartCoroutine(corrutineRotate(hit.point- new Vector3(0, hit.collider.gameObject.transform.localScale.y / 2, 0) ));
                }
                else
                {
                    StartCoroutine(corrutineRotate(hit.point));
                }
                target = hit.point;
            }

        }
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, LayerMask.NameToLayer("Terrain")))
        {
            Quaternion quatDestiny = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation,quatDestiny,Time.deltaTime*5);
        }
        
    }
    IEnumerator corrutineRotate(Vector3 target)
    {
        Quaternion a = pivHead.transform.rotation;
        Quaternion b = Quaternion.LookRotation(target-transform.position,transform.up);
        float t = 0;
        while (0.05f <Quaternion.Angle(pivHead.transform.rotation, b))
        {
            pivHead.transform.rotation = Quaternion.Lerp(a, b, t);
            t += Time.deltaTime*rotationSpeed;
            yield return null;
        }
        
        OnStartShoot?.Invoke();
    }
    public void Shoot()
    {
        GameObject go = Instantiate(goShoot, canonPoint.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce((target - transform.position) * shootForce, ForceMode.Impulse);
    }
}
