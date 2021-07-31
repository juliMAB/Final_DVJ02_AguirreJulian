using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCanvasController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI boxDestoys;
    public TextMeshProUGUI tankDistance;
    public TextMeshProUGUI winOrLose;

    Data myData;
    private void Start()
    {
        myData = DataLogger.Get().LoadData();
        score.text = "Score: "+ myData.Score.ToString();
        int getDestroyBoxes = myData.getDestroyBoxes;
        boxDestoys.text = "Destroy boxes: \n" + getDestroyBoxes.ToString();
        tankDistance.text = "Distance: \n"+myData.DistanceTraveled.ToString();
        switch (myData.Result)
        {
            case Data.END.WIN:
                winOrLose.text = "YOU WIN";
                break;
            case Data.END.LOSE:
                winOrLose.text = "YOU LOSE";
                break;
            case Data.END.none:
                winOrLose.text = "NULL?!";
                break;
            default:
                break;
        }
        
    }   
}       
