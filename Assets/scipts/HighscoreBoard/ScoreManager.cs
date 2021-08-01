using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private Button playButton;
    [SerializeField] private string playerName;
    [SerializeField] private int score;
    private string HighScorePath = "SCOREBOARD.txt";
    int cant = 5;
    int[] scores = new int[5];
    string[] names = new string[5];


    [Serializable]
    class DataHighscore
    {
        public int score = 0;
        public string name = "AAA";
    }
    DataHighscore[] data;

    [SerializeField] private List<TMP_Text> num = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> namesT = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> scoresT = new List<TMP_Text>();

    void Start()
    {
        score = DataLogger.Get().LoadData().Score;
        if (score==0)
        {
            playButton.gameObject.SetActive(false);
            inputName.gameObject.SetActive(false);
        }
        ReadFile();
        ShowScores();
    }

    void Update()
    {
        if (inputName.text == "" || inputName.text.Length != 3)
        {
            playButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(true);
        }
    }
    public void CallButton()
    {
        playerName = inputName.text;
        ReadFile();
        UpdateData();
        ShowScores();
        SaveFile();
    }
    public void WriteData()
    {
        data = new DataHighscore[5];

        for (int i = 0; i < cant; i++)
        {
            data[i] = new DataHighscore();
            data[i].name = names[i];
            data[i].score = scores[i];
        }
    }
    public void ReadData()
    {
        for (int i = 0; i < cant; i++)
        {
            names[i] = data[i].name;
            scores[i] = data[i].score;
        }
    }
    public void SaveFile()
    {
        string path = HighScorePath;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fs = new FileStream(path, FileMode.Create);

        WriteData();
        formatter.Serialize(fs, data);
        fs.Close();
    }
    public void UpdateData()
    {
        SortValues(score, playerName);
    }
    public bool SortValues(int newScore, string newName)
    {
        int[] scores2 = new int[6];
        for (int i = 0; i < cant; i++)
        {
            scores2[i] = scores[i];
            scores2[i + 1] = newScore;
        }
        string[] names2 = new string[6];
        for (int i = 0; i < cant; i++)
        {
            names2[i] = names[i];
            names2[i + 1] = newName;
        }
        for (int z = 1; z < cant + 1; ++z)
        {
            for (int v = 0; v < (cant + 1 - z); v++)
            {
                if (scores2[v] > scores2[v + 1])
                {
                    int aux = scores2[v];
                    string auxname = names2[v];
                    scores2[v] = scores2[v + 1];
                    names2[v] = names2[v + 1];
                    scores2[v + 1] = aux;
                    names2[v + 1] = auxname;
                }
            }
        }
        for (int i = 0; i < cant; i++)
        {
            names[i] = names2[cant - i];
            scores[i] = scores2[cant - i];
        }
        return (score != scores2[cant]);
    }
    public void ReadFile()
    {
        string path = HighScorePath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(fs) as DataHighscore[];
            ReadData();
            fs.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
    public void FirstSave()
    {
        for (int i = 0; i < 5; i++)
        {
            scores[i] = 0;
            names[i] = "AAA";
        }
        SaveFile();
    }
    public void ShowScores()
    {
        for (int i = 0; i < cant; i++)
        {
            num[i].text = i.ToString();
            scoresT[i].text = scores[i].ToString();
            namesT[i].text = names[i].ToString();
        }
    }
}
