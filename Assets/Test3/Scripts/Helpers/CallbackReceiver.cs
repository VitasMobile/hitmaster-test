using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    public class CallbackReceiver : MonoBehaviour
    {
        public UnityEvent callbacks;

        private void Awake()
        {
            if (callbacks == null)
            {
                callbacks = new UnityEvent();
            }
        }

        public void RunCallback()
        {
            callbacks?.Invoke();
        }
    }
}
