using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class scoreBoard : MonoBehaviour
{
    public struct PlayerScore
    {
        public string username;
        public int score;
    }

    [SerializeField] private TMP_Text[] names = new TMP_Text[10];
    [SerializeField] private TMP_Text[] scores = new TMP_Text[10];
    [SerializeField] private TMP_Text[] nums = new TMP_Text[10];
    private List<PlayerScore> hsList = new List<PlayerScore>();
    private string[] hsPrefTexts = new[] { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th" };

    void Start()
    {
        CallTopScore();
        for (int i = 0; i < names.Length; i++)
        {
            hsList.Add(new PlayerScore());
        }

        SetScores();
    }

    void SetScores()
    {
        for (int i = 0; i < names.Length; i++)
        {
            nums[i].text = hsPrefTexts[i];
            if (hsList[i].username != null)
            {
                names[i].text = hsList[i].username;
                scores[i].text = hsList[i].score.ToString();
            }
        }
    }

    public void CallTopScore()
    {
        StartCoroutine(TopScore());
    }

    IEnumerator TopScore()
    {
        string url = DataBD.path + "get_topscore.php";
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        string[] data = webRequest.downloadHandler.text.Split('\t');

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == "")
                break;

            string[] playerData = data[i].Split('#');

            PlayerScore player = new PlayerScore();
            player.username = playerData[0];
            int score;
            if (int.TryParse(playerData[1], NumberStyles.Number, CultureInfo.InvariantCulture, out score))
            {
                player.score = score;
            }

            hsList[i] = player;
        }

        SetScores();
    }
}
