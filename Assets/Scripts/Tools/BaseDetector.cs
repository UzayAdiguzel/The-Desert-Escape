using Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace MarkerGame.Utility
{
    public class BaseDetector<T> : MonoBehaviour
        where T : IObject
    {
        public UnityEvent<T> TriggerEnter;
        public UnityEvent<T> TriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.attachedRigidbody)
                return;

            var id = other.attachedRigidbody.gameObject.GetInstanceID();
            if (!ObjectHelper.TryToGetObj(id, out T target)) return;

            TriggerEnter?.Invoke(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.attachedRigidbody)
                return;

            var id = other.attachedRigidbody.gameObject.GetInstanceID();
            if (!ObjectHelper.TryToGetObj(id, out T target)) return;

            TriggerExit?.Invoke(target);
        }
    }
}