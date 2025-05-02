using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGame/StageData")]
public class StageData : ScriptableObject
{
    public int StageNumber;
    public GameObject monster;
    public int SpawnCount;
    public float spawnInterval;

}
