using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SaveScoreDB : MonoBehaviour
{

    public void CallUpdateActualScoreToBaseData()
    {
        StartCoroutine(PostScore());
    }

    IEnumerator PostScore()
    {
        string path = DataBD.path + "post_score.php";

        WWWForm form = new WWWForm();
        form.AddField("username", DataLogger.Get().username);
        form.AddField("score", DataLogger.Get().LoadData().Score.ToString());
        form.AddField("deaths", DataLogger.Get().LoadData().DeathCount.ToString());
        form.AddField("kills", DataLogger.Get().LoadData().Kills.ToString());
        form.AddField("time", ((int)DataLogger.Get().LoadData().Time).ToString());
        form.AddField("distance", ((int)DataLogger.Get().LoadData().DistanceTraveled).ToString());
        UnityWebRequest www = UnityWebRequest.Post(path, form);
        yield return www.SendWebRequest();
        Debug.Log("Post:" + www.downloadHandler.text);
    }
}
