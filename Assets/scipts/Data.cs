using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public enum END
    {
        WIN,
        LOSE,
        none
    }
    [Header("Data")]
    [SerializeField] int scorePerKill;
    int score;
    int destroyBoxes = 0;
    float distanceTraveled;
    float time;
    int deathCount;
    int kills;
    END result;
    public int ScorePerKill { set { scorePerKill = value; } }
    public int Score { get { return score; } set { score = value; } }
    public int getDestroyBoxes => destroyBoxes;
    public float Time { get { return time; } set { time = value; } }
    public int DeathCount { get { return deathCount; } set { deathCount = value; } }
    public END Result { get { return result; } set { result = value; } }
    public float DistanceTraveled { get { return distanceTraveled; } set { distanceTraveled = value; } }
    public int Kills { get => kills; set => kills = value; }

    public void OnBoxKill()
    {
        score += scorePerKill;
        destroyBoxes++;
        kills++;
    }

    public void ResetData()
    {
        score = 0;
        destroyBoxes = 0;
        distanceTraveled = 0;
        time = 0;
        deathCount = 0;
        result = 0;
    }


}
