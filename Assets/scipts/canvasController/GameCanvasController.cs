using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI life;
    [SerializeField] TextMeshProUGUI InfoText;

    private int lifeF;
    public int LifeF { set { lifeF = value; } }
    private float timef;
    public float TimeF { set { timef = value; } }
    private int scoref;
    public int ScoreF { set { scoref = value; } }
    private void Start()
    {
        score.text = "Score: 0";
        time.text = "Time:\n00:00";
        StartCoroutine(DisableInfo());
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

    public void UpdateLife()
    {
        string newLife = "Life: ";
        newLife += lifeF.ToString();
        life.text = newLife; 
    }
    public IEnumerator DisableInfo()
    {
        float t = 255;
        while (0 < InfoText.color.a)
        {
            t--;
            InfoText.color = new Color(InfoText.color.r, InfoText.color.g, InfoText.color.b, t/255f);
            yield return new WaitForSeconds(0.001f);
        }
        InfoText.gameObject.SetActive(false);
    }

}
