using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

    public PlayerController player { get; private set; }
    private ResourceController resourceController;

    [SerializeField] private int currentWaveIdx = 0;


    private EnemyManager enemyManager;

    private UIManager uiManager;
    public static bool IsFirstLoading = true;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        player.Init(this);
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        uiManager = FindObjectOfType<UIManager>();

        resourceController = player.GetComponent<ResourceController>();
        resourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        resourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }

    private void Start()
    {
        if(!IsFirstLoading)
        {
            StartGame();
        }
        else
        {
            IsFirstLoading = false;
        }
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIdx += 1;
        enemyManager.StartWave(1 + currentWaveIdx / 5);
        uiManager.ChangeWave(currentWaveIdx);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }
}
