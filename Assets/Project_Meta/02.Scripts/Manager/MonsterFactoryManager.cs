using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactoryManager : MonoBehaviour
{
    public static MonsterFactoryManager Instance;

    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    [SerializeField] private Transform poolObjects;

    private void Awake()
    {
        Instance = this;

        if (poolObjects == null)
        {
            GameObject go = new GameObject("PoolObjects");
            poolObjects = go.transform;
        }
    }

    public GameObject SpawnMonster(GameObject monsterPrefab, Vector2 spawnPos, Vector2 dir)
    {
        if (!pools.ContainsKey(monsterPrefab))
        {
            // 없으면 처음 50개 미리 생성
            Queue<GameObject> newPool = new Queue<GameObject>();
            for (int i = 0; i < 50; i++)
            {
                GameObject newMonster = Instantiate(monsterPrefab, poolObjects);
                newMonster.SetActive(false);
                newPool.Enqueue(newMonster);
            }
            pools.Add(monsterPrefab, newPool);
        }

        Queue<GameObject> pool = pools[monsterPrefab];

        // 풀 비어있으면 2배로 확장
        if (pool.Count == 0)
        {
            int expandCount = 50;
            for (int i = 0; i < expandCount; i++)
            {
                GameObject newMonster = Instantiate(monsterPrefab);
                newMonster.SetActive(false);
                pool.Enqueue(newMonster);
            }
        }

        GameObject monsterObj = pool.Dequeue();
        monsterObj.SetActive(true);
        monsterObj.transform.position = spawnPos;

        DumbMonster dumbMonster = monsterObj.GetComponent<DumbMonster>();
        dumbMonster.Init(dir);

       
        dumbMonster.OnDead += (deadMonster) => ReturnMonster(monsterPrefab, deadMonster.gameObject);

        return monsterObj;
    }

    private void ReturnMonster(GameObject monsterPrefab, GameObject monsterObj)
    {
        monsterObj.SetActive(false);
        pools[monsterPrefab].Enqueue(monsterObj);
    }
}
