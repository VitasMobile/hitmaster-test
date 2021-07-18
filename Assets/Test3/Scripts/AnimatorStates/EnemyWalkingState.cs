using UnityEngine;

namespace Test3
{
    public class EnemyWalkingState : StateMachineBehaviour
    {
        private Player _player;
        private Enemy _enemy;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = FindObjectOfType<Player>();
            _enemy = animator.GetComponentInParent<Enemy>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _player.transform.position, _enemy.speed * Time.deltaTime);

            Vector3 direction = (_player.transform.position - _enemy.transform.position).normalized;
            direction.y = 0.0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _enemy.transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, lookRotation, Time.deltaTime * _enemy.rotationSpeed);
        }
    }
}