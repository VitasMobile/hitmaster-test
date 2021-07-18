using System;
using Playrika.GameFoundation.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test3
{
    public class Game : MonoBehaviour
    {
        #region SINGLETON
        private static Game _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);

                Init();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion


        #region EVENTS
        public static event Action<int, int, int> ChangeEnemiesCountEvent;
        #endregion


        private int _levelIndex = 1;

        private World _world;
        public static World World { get => _instance._world; }

        [SerializeField] private Bullet _bulletPrefab;
        public static ObjectPool<Bullet> BulletPool { get; private set; }

        private void Init()
        {
            BulletPool = new ObjectPool<Bullet>(_bulletPrefab, transform, 10);
        }


        #region State machine
        public static event System.Action<GAME_STATE> ChangeGameStateEvent;

        private GAME_STATE _gameState = GAME_STATE.MAIN_MENU;
        public static GAME_STATE CurrentGameState { get => _instance._gameState; }

        public static void SetState(GAME_STATE gameState)
        {
            if (_instance._gameState == GAME_STATE.WAITING && gameState != GAME_STATE.WAITING)
            {
                _instance._world.GetCurrentRoom().Init();
            }
            _instance._gameState = gameState;
            ChangeGameStateEvent?.Invoke(_instance._gameState);
        }
        #endregion


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Room room = _world.GetCurrentRoom();

                switch (CurrentGameState)
                {
                    case GAME_STATE.MAIN_MENU:
                        _world.Player.Go(room.waypoints);
                        _world.NextRoom();
                        SetState(room.gameState);
                        break;

                    case GAME_STATE.GAME:
                        if (room.IsComplete())
                        {
                            _world.Player.Go(room.waypoints);
                            _world.NextRoom();
                        }
                        break;
                }
            }
        }

        public static void UpdateUI()
        {
            int enemiesCount = 0;
            int aliveEnemiesCount = 0;

            foreach (Room room in World.rooms)
            {
                foreach (Enemy enemy in room.enemies)
                {
                    enemiesCount++;
                    if (enemy.IsAlive)
                    {
                        aliveEnemiesCount++;
                    }
                }
            }

            ChangeEnemiesCountEvent?.Invoke(_instance._levelIndex, enemiesCount, aliveEnemiesCount);
        }


        public static void RegisterWorld(World world)
        {
            _instance._world = world;
        }

        public void RestartGame()
        {
            _instance._levelIndex = 1;
            SceneManager.LoadScene("Simple Test3");
        }

        public void NextLevel()
        {
            _instance._levelIndex++;
            SceneManager.LoadScene("Simple Test3");
        }
    }
}
