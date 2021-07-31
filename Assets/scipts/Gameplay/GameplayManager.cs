using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    private Data Mydata = new Data();
    public Action OnGameOver;
    [Header("GameConfig")]
    [SerializeField] float gameTime = 300; // 5 minutos.
    [SerializeField] int scorePerKill;

    [Header("dependencies")]
    [SerializeField] ShipManager player;
    [SerializeField] GameCanvasController canvas;
    [SerializeField] BoxesManager boxesManager;
    private void Start()
    {
        Mydata.ScorePerKill = scorePerKill;
        boxesManager.OnBoxCreated += chargeBoxes;
        boxesManager.dependentStart();
    }
    void UpdateCanvas()
    {
        canvas.ScoreF = Mydata.Score;
        canvas.UpdateScore();
    }
    void chargeBoxes()
    {

        foreach (var item in boxesManager.boxes)
        {
            item.OnBoxKill += Mydata.OnBoxKill;
            item.OnBoxKill += UpdateCanvas;
        }
    }
    void EndGame()
    {
        Mydata.DistanceTraveled= (player.getShipController.TotalDistance);
        DataLogger.Get().SaveData(Mydata);

    }
    private void Update()
    {
        gameTime -= Time.deltaTime;
        canvas.TimeF = gameTime;
        canvas.UpdateTime();
    }
}
