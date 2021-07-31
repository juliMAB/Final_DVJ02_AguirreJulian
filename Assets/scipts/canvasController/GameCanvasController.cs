using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;

    private float timef;
    public float TimeF { set { timef = value; } }
    private int scoref;
    public int ScoreF { set { scoref = value; } }
    private void Start()
    {
        score.text = "Score: 0";
        time.text = "Time:\n00:00"; 
    }
    public void UpdateScore()
    {
        string newScore= "Score: ";
        newScore += scoref.ToString();
        score.text = newScore;
    }
    public void UpdateTime()
    {
        string newTime = "Time:\n";
        int min = (int)timef / 60;
        int seg = (int)timef % 60;

        if (min < 10)
        {
            newTime += "0" + min;
            if (seg<10)
            {
                newTime += ":0" + seg;
            }
            else
            {
                newTime += ":"  + seg;
            }
        }
        else
        {
            newTime += min;
            if (seg < 10)
            {
                newTime += ":0" + seg;
            }
            else
            {
                newTime += ":" +  seg;
            }
        }
        time.text = newTime;
    }

}
