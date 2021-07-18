using UnityEngine;

namespace Test3
{
    public class Bullet : MonoBehaviour
    {
        private World _world;
        public float speed = 200.0f;
        public int minDamage = 3;
        public int maxDamage = 10;
        private Rigidbody _rigidbody;
        public readonly float _maxLife = 3.0f;
        private float _life;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _world = FindObjectOfType<World>();
        }

        private void FixedUpdate()
        {
            if (_life > 0.0f)
            {
                _life -= Time.fixedDeltaTime;
                if (_life <= 0.0f)
                {
                    Kill();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Kill();

            if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hit))
            {
                hit.Hit(Random.Range(minDamage, maxDamage));
            }
        }

        public void Shoot()
        {
            _life = _maxLife;
            _rigidbody.AddForce(transform.forward * speed);
        }

        public void Kill()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            Game.BulletPool.Push(this);
        }
    }
}
