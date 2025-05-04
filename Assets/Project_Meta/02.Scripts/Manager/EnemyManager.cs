using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace Meta
{
    public class EnemyManager : MonoBehaviour
    {
        public List<Rect> spawnAreas;
        private Color gizmoColror = Color.red;


        public void Spawn(GameObject monster, Transform player)
        {

            Vector2 ranPos = RandomPos();
            Vector2 dir = ((Vector2)player.position - ranPos).normalized;
            MonsterFactoryManager.Instance.SpawnMonster(monster, ranPos, dir);
        
        }

        
        private Vector2 RandomPos()
        {
            Rect area = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Vector2 spawnPos = new Vector2(
                Random.Range(area.xMin, area.xMax),
                Random.Range(area.yMin, area.yMax)
            );

            return spawnPos;
        }

        private void OnDrawGizmosSelected()
        {
            if (spawnAreas == null) return;

            Gizmos.color = gizmoColror;
            foreach (var area in spawnAreas)
            {
                Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
                Vector3 size = new Vector3(area.width, area.height);

                Gizmos.DrawCube(center, size);
            }
        }

    }

}
