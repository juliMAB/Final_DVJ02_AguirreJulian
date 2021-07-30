using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Action<float> OnMove;
    [SerializeField]float velocity;
    [SerializeField] float angleVelocity;
    private Rigidbody rigidbody;
    GameObject canon;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.up * Time.deltaTime * -angleVelocity);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up*Time.deltaTime*angleVelocity);
        }
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            transform.Translate(transform.forward*Time.deltaTime*velocity);
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            transform.Translate(transform.forward * Time.deltaTime * -velocity);
        }
        //if (Input.GetMouseButton((int)KeyCode.Mouse0))
        //{
        //    //disparar.
        //}
    }
    void EngineRunning()
    {
        rigidbody.AddForce(transform.forward * velocity);
        //OnMove(Mathf.Abs(transform.forward.z * velocity));
    }
}
