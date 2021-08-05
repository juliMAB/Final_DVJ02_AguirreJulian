using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    [SerializeField] int scorePerKill;
    public int ScorePerKill { set { scorePerKill = value; } }
    public enum END
    {
        WIN,
        LOSE,
        none
    }
    int score;
    public int Score { get { return score; } set { score = value; } }

    int destroyBoxes=0;
    public int getDestroyBoxes => destroyBoxes;

    float distanceTraveled;

    float time;

    public float Time { get { return time; } set { time = value; } }

    END result;
    public END Result { get { return result; } set { result = value; } }
    public float DistanceTraveled { get { return distanceTraveled; } set { distanceTraveled = value; } }

    public void OnBoxKill()
    {
        score += scorePerKill;
        destroyBoxes++;
    }
}
