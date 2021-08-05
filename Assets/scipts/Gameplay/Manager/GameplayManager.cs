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
    [SerializeField] EnemySpawner enemyspawn;
    int boxesCuantity;
    float actualGameTime;
    private void Start()
    {
        actualGameTime = gameTime;
        Mydata.ScorePerKill = scorePerKill;
        boxesManager.OnBoxCreated += chargeBoxes;
        boxesManager.dependentStart();
        player.getShipController.OnDeath += MyTankDeath;
        player.getShipController.OnDamage += UpdateCanvasLife;
        enemyspawn.OnEnemyCreate += AddTank;
    }
    private void AddTank()
    {
        enemyspawn.lastTank.OnDeath+= Mydata.OnBoxKill;
        enemyspawn.lastTank.OnDeath+= UpdateCanvasScore;
    }
    void MyTankDeath()
    {
        Mydata.Result = Data.END.LOSE;
        EndGame();
    }
    void UpdateCanvasScore()
    {
        canvas.ScoreF = Mydata.Score;
        canvas.UpdateScore();
    }
    void UpdateCanvasLife()
    {
        canvas.LifeF = player.getShipController.Lives;
        canvas.UpdateLife();
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
            item.OnBoxKill += UpdateCanvasScore;
            item.OnBoxKill += allBoxesDestoy;
        }
    }
    void EndGame()
    {
        Mydata.DistanceTraveled= (player.getShipController.TotalDistance);
        Mydata.Time = gameTime - actualGameTime;
        DataLogger.Get().SaveData(Mydata);
        gameSceneController.LoadEnd();
    }
    private void Update()
    {
        actualGameTime -= Time.deltaTime;
        canvas.TimeF = actualGameTime;
        canvas.UpdateTime();
        if (actualGameTime < 0)
        {
            Mydata.Result = Data.END.LOSE;
            EndGame();
        }
    }
}
