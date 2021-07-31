using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviourSingletonScript;

public class DataLogger : MonoBehaviourSingleton<DataLogger>
{
    Data myData;
    private void Start()
    {
        
    }

    public void SaveData(Data data)
    {
        myData = data; //la info del player.
    }
}
