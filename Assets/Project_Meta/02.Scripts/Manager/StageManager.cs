using Meta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
  
    static public StageManager instance;

    public StageData currentStageData;
    public Meta.EnemyManager enemyManager;
    public Transform player;

    private void OnEnable()
    {
        Meta.GameManager.OnGameStart += StartStage;
    }

    private void OnDisable()
    {
        Meta.GameManager.OnGameStart -= StartStage;
    }

    private void Awake()
    {
        instance = this; 
        
    }
    public void StartStage()
    {
        Debug.Log("Stage Start");
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        int spawnedCount = 0;

        while (spawnedCount < currentStageData.SpawnCount)
        {
            enemyManager.Spawn(currentStageData.monster, player);
            spawnedCount++;

            yield return new WaitForSeconds(currentStageData.spawnInterval);
        }
    }
}


