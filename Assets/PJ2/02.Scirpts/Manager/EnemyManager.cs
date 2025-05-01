using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColror = new Color(1, 0, 0, .3f);
    private List<EnemyController> activeEnemies = new List<EnemyController>();

    private bool enemySpawnCompliete;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;
        
    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public void StartWave(int waveCount)
    {
        if(waveCount <= 0)
        {
            gameManager.EndOfWave();
            return;
        }

        if(waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }


    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnCompliete = false;
        yield return new WaitForSeconds(timeBetweenSpawns);

        for(int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        enemySpawnCompliete = true;

    }

    private void SpawnRandomEnemy()
    {
       if(enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("¼³Á¤ x");
            return;
        }

        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];


        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        Vector2 randomPos = new Vector2
            (
                Random.Range(randomArea.xMin, randomArea.xMax),
                Random.Range(randomArea.yMin, randomArea.yMax));

        GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPos.x, randomPos.y), Quaternion.identity);
        EnemyController enemyController = spawnEnemy.GetComponent<EnemyController>();
        enemyController.Init(this, gameManager.player.transform);

        activeEnemies.Add(enemyController);

    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;
         
        Gizmos.color = gizmoColror;
        foreach(var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);

        if(enemySpawnCompliete && activeEnemies.Count == 0)
            gameManager.EndOfWave();
    
    }
}
