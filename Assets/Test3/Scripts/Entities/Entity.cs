using UnityEngine;

namespace Test3
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        public float speed = 2.0f;
        public float rotationSpeed = 10.0f;
        public int hp = 100;
        public bool IsAlive { get => hp > 0; }


        protected virtual void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }
}