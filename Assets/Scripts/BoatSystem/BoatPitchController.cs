using System;
using UnityEngine;

namespace BoatSystem
{
    public class BoatPitchController : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform root;
        [SerializeField] private Transform body;
        [SerializeField] private Transform frontTransform;
        [SerializeField] private Transform backTransform;
        [SerializeField] private float boatHeightOffset;


        private Vector3 _frontHitPoint;
        private Vector3 _backHitPoint;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(_frontHitPoint, .5f);
            Gizmos.DrawSphere(_backHitPoint, .5f);
        }

        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            CheckFront();
            CheckBack();
            ExecuteHeight();
        }

        private void ExecuteHeight()
        {
            var targetDir = _frontHitPoint - _backHitPoint;
            var angle = Vector3.Angle(root.forward, targetDir);
            body.position = new Vector3(body.position.x, _backHitPoint.y, body.position.z);

            body.eulerAngles = _frontHitPoint.y > _backHitPoint.y
                ? new Vector3(-angle, body.eulerAngles.y, body.eulerAngles.z)
                : new Vector3(angle, body.eulerAngles.y, body.eulerAngles.z);
        }

        private void CheckFront()
        {
            var frontPosition = frontTransform.position;
            frontPosition.y += 999f;
            var ray = new Ray(frontPosition, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
            {
                _frontHitPoint = hitInfo.point;
                _frontHitPoint.y += boatHeightOffset;
            }
        }

        private void CheckBack()
        {
            var backPosition = backTransform.position;
            backPosition.y += 999f;
            var ray = new Ray(backPosition, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
            {
                _backHitPoint = hitInfo.point;
                _backHitPoint.y += boatHeightOffset;
            }
        }
    }
}