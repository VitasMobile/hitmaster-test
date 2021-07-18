using UnityEngine;
using UnityEngine.UI;

namespace Test3
{
    public class Enemy : Entity, IHitable
    {
        private Player _player;
        [SerializeField] private Collider _collider;
        [SerializeField] private Slider _hpBar;
        [SerializeField] private RagdollController _ragdoll;


        protected override void Start()
        {
            base.Start();

            _player = FindObjectOfType<Player>();
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();
            }
        }


        private void FixedUpdate()
        {
            if (!IsAlive)
            {
                return;
            }


            if (_player.IsAlive)
            {
                _animator.SetFloat("distance", Vector3.Distance(_player.transform.position, transform.position));
            }
            else
            {
                _animator.SetFloat("distance", 20.0f);
            }
        }

        public void Hit(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                _collider.enabled = false;

                if (_ragdoll)
                {
                    _ragdoll.DoRagdoll(true);
                }

                Game.UpdateUI();
            }

            UpdateHealthBar();

            _animator.SetTrigger("hit");
            _animator.SetInteger("hp", hp);
        }

        private void UpdateHealthBar()
        {
            _hpBar.value = hp * 0.01f;
            _hpBar.gameObject.SetActive(hp > 0);
        }

        public void Strike()
        {
            _player.Hit(Random.Range(10, 30));
        }
    }
}
