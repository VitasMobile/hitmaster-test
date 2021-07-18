using UnityEngine;

namespace Test3
{
    public class World : MonoBehaviour
    {
        [SerializeField] private int _currentRoomIndex = 0;
        public Room[] rooms;

        [SerializeField] private Player _player;
        public Player Player { get => _player; }


        private void Start()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
            }

            Game.RegisterWorld(this);
            Game.SetState(GAME_STATE.MAIN_MENU);

            Game.UpdateUI();
        }

        public Room GetCurrentRoom()
        {
            return rooms[_currentRoomIndex];
        }

        public void NextRoom()
        {
            if (_currentRoomIndex < rooms.Length)
            {
                _currentRoomIndex++;
            }
        }
    }
}
