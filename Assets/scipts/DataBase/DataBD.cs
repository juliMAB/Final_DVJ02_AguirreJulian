using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBD : MonoBehaviourSingletonScript.MonoBehaviourSingleton<DataBD>
{
    public string UserName;
    public const string path = "http://localhost/shooter/";
}
