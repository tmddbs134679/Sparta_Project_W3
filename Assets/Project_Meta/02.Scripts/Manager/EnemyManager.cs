using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meta
{
    public class EnemyManager : MonoBehaviour
    {
        public List<Rect> spawnAreas;
        private Color gizmoColror = Color.red;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
