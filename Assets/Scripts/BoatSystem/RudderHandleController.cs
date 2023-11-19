using System;
using UnityEngine;

namespace BoatSystem
{
    public class RudderHandleController : MonoBehaviour
    {
        [SerializeField] private Transform rudderCastTransform;
        [SerializeField] private Transform rudderHandleModelTransform;

        [SerializeField] private LayerMask layerMask;

        public event Action<Vector3> OnRotation;

        private Vector3 _modelDefaultScale;
        private Camera _camera;
        private bool _isHitRudder;
        private Vector3 _rudderCastDefaultPosition;
        private Vector3 _firstPosition;
        private Vector3 _lastPosition;
        private Vector3 _direction;

        private void Awake()
        {
            _camera = Camera.main;
            _isHitRudder = false;
            _rudderCastDefaultPosition = rudderCastTransform.localPosition;
            _modelDefaultScale = rudderHandleModelTransform.localScale;
        }

        private void Update()
        {
            CheckHandleTouch();
            ExecuteMovement();
            CheckHandleUp();
        }

        private void CheckHandleTouch()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(mouseRay, out var hitInfo, Mathf.Infinity, layerMask)) return;
            //rudderCastTransform.position = hitInfo.point;
            _firstPosition = rudderCastTransform.InverseTransformPoint(hitInfo.point);
            rudderHandleModelTransform.localScale = _modelDefaultScale * 5f;
            _isHitRudder = true;
        }

        private void ExecuteMovement()
        {
            if (!_isHitRudder) return;
            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hitInfo, Mathf.Infinity, layerMask))
            {
                _lastPosition = rudderCastTransform.InverseTransformPoint(hitInfo.point);
                _direction = _firstPosition - _lastPosition;
            }

            OnRotation?.Invoke(_direction);
        }

        private void CheckHandleUp()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            if (!_isHitRudder) return;
            rudderCastTransform.localPosition = _rudderCastDefaultPosition;
            rudderHandleModelTransform.localScale = _modelDefaultScale;
            _isHitRudder = false;
            OnRotation?.Invoke(Vector3.zero);
        }
    }
}