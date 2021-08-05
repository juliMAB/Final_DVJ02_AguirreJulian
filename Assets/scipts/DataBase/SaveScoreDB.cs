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
        form.AddField("score", DataLogger.Get().LoadData().Score);
        form.AddField("deaths", DataLogger.Get().LoadData().DeathCount);
        form.AddField("kills", DataLogger.Get().LoadData().Kills);
        form.AddField("time", (int)DataLogger.Get().LoadData().Time);
        form.AddField("distance", (int)DataLogger.Get().LoadData().DistanceTraveled);
        UnityWebRequest www = UnityWebRequest.Post(path, form);
        yield return www.SendWebRequest();
        Debug.Log("Post:" + www.downloadHandler.text);
    }
}
