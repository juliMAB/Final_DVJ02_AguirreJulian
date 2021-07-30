using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvasController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;

    private void Start()
    {
        score.text = "Score: 0";
        time.text = "Time:\n00:00"; 
    }
    void UpdateScore()
    {
        string newScore= "Score: ";
        newScore += "10";
        score.text = newScore;
    }
}
