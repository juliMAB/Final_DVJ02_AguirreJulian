using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Action<float> OnMove;
    [SerializeField]float velocity;
    [SerializeField] float angleVelocity;
    Rigidbody rb;
    [SerializeField] GameObject canon;
    [SerializeField] GameObject canonPoint;
    [SerializeField] GameObject goShoot;
    [SerializeField] float shootForce;
    List<Vector3> targets = new List<Vector3>();
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
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
            transform.position += (transform.forward * Time.deltaTime * velocity);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += (transform.forward * Time.deltaTime * -velocity);
        }
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                StopAllCoroutines();
                StartCoroutine(corrutineRotate(hit.point));
            }

        }
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, LayerMask.NameToLayer("Terrain")))
        {
            Quaternion quatDestiny = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = quatDestiny;
        }
        
    }
    IEnumerator corrutineRotate(Vector3 target)
    {
        //Quaternion a = canon.transform.rotation;
        Quaternion b = Quaternion.LookRotation(target);
        float t = 0;
        while (0.5f <Quaternion.Angle(canon.transform.rotation, b))
        {
            canon.transform.rotation = Quaternion.Lerp(canon.transform.rotation, b, t);
            t += Time.deltaTime;
            yield return null;
        }
        GameObject go= Instantiate(goShoot, canonPoint.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(target* shootForce, ForceMode.Impulse);
    }
}
