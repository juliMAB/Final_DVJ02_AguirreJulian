using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Rank : MonoBehaviour
{
    List<int> allScores = new List<int>();
    [SerializeField] TextMeshProUGUI textRank;
    int playerScore;
    int rank = -1;

    void Start()
    {
        playerScore = DataLogger.Get().LoadData().Score;
        StartCoroutine(DownloadScores());
    }

    IEnumerator DownloadScores()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(DataBD.path + "get_score.php");

        yield return webRequest.SendWebRequest();

        string[] data = webRequest.downloadHandler.text.Split(',');

        for (int i = 0; i < data.Length; i++)
        {
            int score;
            if (int.TryParse(data[i], NumberStyles.Number, CultureInfo.InvariantCulture, out score))
            {
                allScores.Add(score);
            }
        }
        FindPositionOnRank();
    }
    void FindPositionOnRank()
    {
        for (int i = 0; i < allScores.Count; i++)
        {
            if (playerScore > allScores[i])
            {
                rank = i;
                break;
            }
        }

        if (rank < 0)
            rank = allScores.Count;
        textRank.text = "Rank: " + rank;
    }
}
