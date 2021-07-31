using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 smothness;
    [SerializeField] Vector3 rotare;

    private void LateUpdate()
    {
        transform.position = target.transform.position + smothness;
        transform.eulerAngles = rotare;
    }
}
