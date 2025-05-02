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
            Rect area = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Vector2 spawnPos = new Vector2(
                Random.Range(area.xMin, area.xMax),
                Random.Range(area.yMin, area.yMax)
            );

            GameObject monsterObj = Instantiate(monster, spawnPos, Quaternion.identity);
            DumbMonster dumbmonster = monsterObj.GetComponent<DumbMonster>();

            Vector2 dir = ((Vector2)player.position - spawnPos).normalized;
            dumbmonster.Init(dir);
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
