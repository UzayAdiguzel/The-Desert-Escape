using System;
using DG.Tweening;
using Tools.Utility;
using UnityEngine;

namespace BoatSystem
{
    public class BoatHarpoonController : MonoBehaviour
    {
        [SerializeField] private BoatHarpoonInputController inputController;
        [SerializeField] private float maxRange = 30f;
        [SerializeField] private float lanceDuration = .6f;

        [SerializeField] private Transform harpoonPivot;
        [SerializeField] private Lance lance;
        [SerializeField] private LayerMask layerMask;

        private RaycastHit[] _hitResults = new RaycastHit[10];

        private Vector3 _defaultPosition;
        private Vector3 _worldPosition;
        private Vector3 _direction;
        private float _hitPower;
        private float _range;

        private void OnDrawGizmos()
        {
            ExtDebug.DrawBoxCastBox(_worldPosition, new Vector3(1f, 10f, 1f), Quaternion.identity, _direction,
                _hitPower * 10, Color.blue);
        }

        private void Awake()
        {
            _defaultPosition = lance.transform.localPosition;
            _range = maxRange / 3f;
            inputController.OnRelease += FireOnRelease;
        }

        private void FireOnRelease(float hitPower, Vector3 inverseDirection, Vector3 worldPosition)
        {
            _worldPosition = worldPosition;
            _hitPower = hitPower;
            var direction = harpoonPivot.position - worldPosition;
            direction = direction.normalized;
            _direction = direction;

            var size = Physics.BoxCastNonAlloc(worldPosition, new Vector3(1f, 15f, 1f), direction, _hitResults,
                Quaternion.identity, hitPower * _range, layerMask);

            if (size > 0)
            {
                var calculatedDuration = Vector3
                    .Distance(lance.transform.position, _hitResults[0].point)
                    .Remap(0f, maxRange, 0f, lanceDuration);
                lance.Launch(_defaultPosition, _hitResults[0].point + direction * .5f, calculatedDuration);
            }
            else
            {
                lance.Launch(_defaultPosition, _worldPosition + direction * _range, lanceDuration / 3f);
            }
        }
    }
}