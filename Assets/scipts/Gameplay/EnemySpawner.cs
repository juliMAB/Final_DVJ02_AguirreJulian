using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnerPoints;
    [SerializeField] float timeToSpawn;
    float timeD;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform player;
    public EnemyBombTank lastTank;
    public System.Action OnEnemyCreate=null;
    int index=0;
    void SpawnEnemy()
    {
        GameObject go =  Instantiate(enemy, spawnerPoints[index].transform.position, Quaternion.identity, null);
        lastTank = go.GetComponent<EnemyBombTank>();
        lastTank.player= player;
        OnEnemyCreate?.Invoke();
    }
    private void Start()
    {
        timeD = timeToSpawn;
    }
    private void Update()
    {
        timeD -= Time.deltaTime;
        if (timeD<0)
        {
            index++;
            if (index>=spawnerPoints.Count)
            {
                index = 0;
            }
            SpawnEnemy();
            timeD = timeToSpawn;
        }
    }
}
