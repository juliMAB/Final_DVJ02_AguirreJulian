using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviourSingletonScript;

public class DataLogger : MonoBehaviourSingleton<DataLogger>
{
    data myData;
    private void Start()
    {
        
    }

    public void SaveData(data data)
    {
        myData = data; //la info del player.
    }
}
