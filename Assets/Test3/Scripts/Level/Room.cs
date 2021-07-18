using UnityEngine;

namespace Test3
{
    public class Room : MonoBehaviour
    {
        public Transform[] waypoints;
        public Enemy[] enemies;
        public GAME_STATE gameState = GAME_STATE.GAME;

        [SerializeField] private GameObject[] _inactiveObjects;
        [SerializeField] private GameObject[] _activeObjects;

        public void Init()
        {
            foreach (GameObject go in _inactiveObjects)
            {
                go.SetActive(false);
            }

            foreach(GameObject go in _activeObjects)
            {
                go.SetActive(true);
            }
        }

        public bool IsComplete()
        {
            bool result = false;

            foreach (Enemy enemy in enemies)
            {
                result |= enemy.IsAlive;
            }

            return !result;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (waypoints.Length == 0)
            {
                return;
            }

            Gizmos.color = Color.green;
            foreach (Transform waypoint in waypoints)
            {
                Gizmos.DrawSphere(waypoint.position, 0.3f);
            }


            Gizmos.color = Color.red;
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
        #endif
    }
}
