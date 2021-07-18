using UnityEngine;

namespace Test3
{
    public class PlayerWalkingState : StateMachineBehaviour
    {
        private Player _player;
        private Transform[] _waypoints;
        private int _currentWaypointIndex = 0;


        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = FindObjectOfType<Player>();
            _waypoints = _player.Waypoints;
            _currentWaypointIndex = 0;
            Game.SetState(GAME_STATE.WAITING);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_currentWaypointIndex < 0 || _waypoints.Length == 0)
            {
                return;
            }

            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _waypoints[_currentWaypointIndex].position, _player.speed * Time.deltaTime);

            _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, _waypoints[_currentWaypointIndex].rotation, Time.deltaTime * _player.rotationSpeed);

            if (_player.transform.position == _waypoints[_currentWaypointIndex].position)
            {
                _currentWaypointIndex++;
                if (_currentWaypointIndex >= _waypoints.Length)
                {
                    _currentWaypointIndex = -1;
                    animator.SetBool("walk", false);
                    Game.SetState(Game.World.GetCurrentRoom().gameState);
                }
            }
        }
    }
}