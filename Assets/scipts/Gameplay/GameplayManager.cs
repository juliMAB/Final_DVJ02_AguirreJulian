using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    data Mydata;
    float time;
    [SerializeField]Player Player;
    void EndGame()
    {
        DataLogger.Get().SaveData(Mydata);
    }
}
