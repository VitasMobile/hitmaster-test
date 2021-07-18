using UnityEngine;

namespace Test3
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Rigidbody[] _allRigidbodies;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider[] _allColliders;


        private void Awake()
        {
            if (!_mainCollider)
            {
                _mainCollider = GetComponent<Collider>();
            }
            _allColliders = GetComponentsInChildren<Collider>();
            _allRigidbodies = GetComponentsInChildren<Rigidbody>();

            DoRagdoll(false);
        }

        public void DoRagdoll(bool isRagdoll)
        {
            foreach (Collider collider in _allColliders)
            {
                collider.enabled = isRagdoll;
            }

            foreach (Rigidbody rigidbody in _allRigidbodies)
            {
                rigidbody.useGravity = isRagdoll;
                rigidbody.isKinematic = !isRagdoll;
            }

            _mainCollider.enabled = !isRagdoll;
            if (_rigidbody)
            {
                _rigidbody.useGravity = !isRagdoll;
            }
            _animator.enabled = !isRagdoll;
        }
    }
}
