using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactoryManager : MonoBehaviour
{
    public static MonsterFactoryManager Instance;

    private Dictionary<GameObject, PoolFactory<DumbMonster>> pools = new Dictionary<GameObject, PoolFactory<DumbMonster>>();

    [SerializeField] private Transform poolObjects;
    private readonly int MONSTERCOUNT = 50;

    private void Awake()
    {
        Instance = this;

        if (poolObjects == null)
        {
            GameObject go = new GameObject("MonsterPoolObjects");
            poolObjects = go.transform;
        }
    }

    public DumbMonster SpawnMonster(GameObject monsterPrefab, Vector2 spawnPos, Vector2 dir)
    {
        if (!pools.ContainsKey(monsterPrefab))
        {
            var pool = new PoolFactory<DumbMonster>(monsterPrefab, poolObjects, MONSTERCOUNT);
            pools.Add(monsterPrefab, pool);
        }

        var monster = pools[monsterPrefab].Get(spawnPos);
        monster.Init(dir);
        monster.OnDead += (monster) => pools[monsterPrefab].Return(monster);

        return monster;
    }
}
