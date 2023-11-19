using System;
using UnityEngine;

namespace BoatSystem
{
    public class BoatHarpoonInputController : MonoBehaviour
    {
        [SerializeField] private Lance lance;
        [SerializeField] private Transform harpoonCastTransform;
        [SerializeField] private Transform harpoonBack;
        [SerializeField] private Transform harpoonRoot;
        [SerializeField] private Transform harpoonHandleModelTransform;
        [SerializeField] private float maxPower;
        [SerializeField] private LayerMask layerMask;

        public event Action<float, Vector3, Vector3> OnRelease;

        private Vector3 _modelDefaultScale;
        private Camera _camera;
        private bool _isHitHarpoon;
        private Vector3 _harpoonCastDefaultPosition;
        private Vector3 _harpoonHandleDefaultPosition;
        private Vector3 _firstPosition;
        private Vector3 _lastPosition;
        private float _hitPower;

        private void Awake()
        {
            _camera = Camera.main;
            _isHitHarpoon = false;
            _harpoonCastDefaultPosition = harpoonCastTransform.localPosition;
            _harpoonHandleDefaultPosition = harpoonBack.localPosition;
            _modelDefaultScale = harpoonHandleModelTransform.localScale;
        }

        private void Update()
        {
            CheckHandleTouch();
            ExecuteMovement();
            CheckHandleUp();
        }

        private void CheckHandleTouch()
        {
            if (!lance.LanceReady) return;
            if (!Input.GetMouseButtonDown(0)) return;
            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(mouseRay, out var hitInfo, Mathf.Infinity, layerMask)) return;
            //rudderCastTransform.position = hitInfo.point;
            _firstPosition = harpoonCastTransform.InverseTransformPoint(hitInfo.point);
            harpoonHandleModelTransform.localScale = _modelDefaultScale * 15f;
            _isHitHarpoon = true;
        }

        private void ExecuteMovement()
        {
            if (!_isHitHarpoon) return;
            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hitInfo, Mathf.Infinity, layerMask))
            {
                _lastPosition = harpoonCastTransform.InverseTransformPoint(hitInfo.point);
                _hitPower = Vector3.Distance(_firstPosition, _lastPosition);
                if (_hitPower > maxPower) _hitPower = maxPower;
                var direction = (_firstPosition - _lastPosition).normalized;
                if (direction != Vector3.zero)
                    harpoonRoot.localRotation = Quaternion.LookRotation(direction);
                harpoonBack.localPosition =
                    Vector3.ClampMagnitude(harpoonRoot.InverseTransformPoint(hitInfo.point), 3f);
            }
        }

        private void CheckHandleUp()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            if (!_isHitHarpoon) return;
            if (_hitPower > .5f)
                OnRelease?.Invoke(_hitPower, harpoonBack.localPosition, harpoonBack.position);
            harpoonCastTransform.localPosition = _harpoonCastDefaultPosition;
            harpoonBack.localPosition = _harpoonHandleDefaultPosition;
            harpoonHandleModelTransform.localScale = _modelDefaultScale;
            _isHitHarpoon = false;
        }
    }
}