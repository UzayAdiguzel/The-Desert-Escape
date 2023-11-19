using System;
using Tools.Utility;
using Unity.Mathematics;
using UnityEngine;

namespace BoatSystem
{
    public class RudderRotationController : MonoBehaviour
    {
        [SerializeField] private Transform rudderPivot;
        [SerializeField] private RudderHandleController handleController;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float2 rotationRange;
        [SerializeField] private float recoveryValue;

        private float _rotateValue;
        private float _yRot;

        public float RudderDirection { get; set; }

        private void Awake()
        {
            handleController.OnRotation += FireOnRotation;
        }

        private void Update()
        {
            _yRot += _rotateValue * rotateSpeed * Time.deltaTime;
            if (_yRot > rotationRange.y)
                _yRot = rotationRange.y;

            if (_yRot < rotationRange.x)
                _yRot = rotationRange.x;
            rudderPivot.localRotation = Quaternion.Euler(0f, _yRot, 0f);
            if (_yRot < 0)
                _yRot += Time.deltaTime * recoveryValue;

            if (_yRot > 0)
                _yRot -= Time.deltaTime * recoveryValue;

            RudderDirection = _yRot.Remap(rotationRange.x, rotationRange.y, 1, -1);
        }

        private void FireOnRotation(Vector3 direction)
        {
            _rotateValue = direction.x;
        }
    }
}