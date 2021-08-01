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
    [SerializeField] GameSceneController gameSceneController;
    int boxesCuantity;
    private void Start()
    {
        Mydata.ScorePerKill = scorePerKill;
        boxesManager.OnBoxCreated += chargeBoxes;
        boxesManager.dependentStart();
        player.getShipController.OnDeath += MyTankDeath;
    }
    void MyTankDeath()
    {
        Mydata.Result = Data.END.LOSE;
        EndGame();
    }
    void UpdateCanvas()
    {
        canvas.ScoreF = Mydata.Score;
        canvas.UpdateScore();
    }
    void allBoxesDestoy()
    {
        if (Mydata.getDestroyBoxes== boxesCuantity)
        {
            Mydata.Result = Data.END.WIN;
            EndGame();
        }
    }
    void chargeBoxes()
    {

        foreach (var item in boxesManager.boxes)
        {
            boxesCuantity++;
            item.OnBoxKill += Mydata.OnBoxKill;
            item.OnBoxKill += UpdateCanvas;
            item.OnBoxKill += allBoxesDestoy;
        }
    }
    void EndGame()
    {
        Mydata.DistanceTraveled= (player.getShipController.TotalDistance);
        DataLogger.Get().SaveData(Mydata);
        gameSceneController.LoadEnd();
    }
    private void Update()
    {
        gameTime -= Time.deltaTime;
        canvas.TimeF = gameTime;
        canvas.UpdateTime();
        if (gameTime<0)
        {
            Mydata.Result = Data.END.LOSE;
            EndGame();
        }
    }
}
