using Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
  
    static public StageManager instance;

    public List<StageData> stageDatas;
    public int currentStageIndex = 0;
    private StageData CurrentStageData => stageDatas[currentStageIndex];
    public Meta.EnemyManager enemyManager;
    public Transform player;
    private int aliveEnemies = 0;

    public event Action OnStageClear;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        Meta.GameManager.OnGameStart += StartStage;
    }

    private void OnDisable()
    {
        Meta.GameManager.OnGameStart -= StartStage;
    }


    public void StartStage()
    {
        Debug.Log("Stage Start");
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        int spawnedCount = 0;

        while (spawnedCount < CurrentStageData.SpawnCount)
        {
            enemyManager.Spawn(CurrentStageData.monster, player);
            spawnedCount++;
            aliveEnemies++;
            yield return new WaitForSeconds(CurrentStageData.spawnInterval);
        }

        OnStageClear?.Invoke();
    }

    public void NextStage()
    {
        currentStageIndex++;

        if (currentStageIndex >= stageDatas.Count)
        {
            return;
            currentStageIndex = 0;
        }

        Meta.GameUI gameUI = Meta.UIManager.Instance.GetUI<Meta.GameUI>(EUISTATE.HOME);
        gameUI.UpdateStage(currentStageIndex + 1);
        StartStage();
    }
}


