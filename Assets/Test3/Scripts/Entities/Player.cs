using UnityEngine;

namespace Test3
{
    public class Player : Entity, IHitable
    {
        [SerializeField] private Transform _pistolHole;
        private Camera _mainCamera;
        public Transform[] Waypoints { get; private set; } = null;


        protected override void Start()
        {
            base.Start();

            _mainCamera = Camera.main;
            Game.ChangeGameStateEvent += OnChangedGameState;
        }

        private void OnDestroy()
        {
            Game.ChangeGameStateEvent -= OnChangedGameState;
        }

        private void OnChangedGameState(GAME_STATE gameState)
        {
            if (gameState == GAME_STATE.COMPLETED)
            {
                _animator.SetTrigger("dancing");
            }
        }

        private void Update()
        {
            if (!IsAlive)
            {
                return;
            }

            if (Game.CurrentGameState != GAME_STATE.GAME && Game.CurrentGameState != GAME_STATE.WAITING)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            Vector3 targetPosition;

            Bullet bullet = Game.BulletPool.Pop();
            if (bullet)
            {
                bullet.transform.position = _pistolHole.position;
                bullet.transform.rotation = _pistolHole.rotation;
            }

            Vector3 mousePosition = Input.mousePosition;

            RaycastHit raycastHit;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                targetPosition = raycastHit.point;
            }
            else
            {
                targetPosition = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10.0f));
            }

            bullet.transform.LookAt(targetPosition);
            Debug.DrawRay(bullet.transform.position, bullet.transform.forward * 500.0f, Color.green, 5.0f);
            bullet.Shoot();
        }

        public void Hit(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                Game.SetState(GAME_STATE.GAME_OVER);
            }
            _animator.SetInteger("hp", hp);
            _animator.SetTrigger("hit");
        }

        public void Go(Transform[] waypoints)
        {
            Waypoints = waypoints;
            _animator.SetBool("walk", true);
        }
    }
}