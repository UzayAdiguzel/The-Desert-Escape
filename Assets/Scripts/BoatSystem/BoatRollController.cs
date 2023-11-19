using UnityEngine;
using UnityEngine.Serialization;

namespace BoatSystem
{
    public class BoatRollController : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform root;
        [SerializeField] private Transform rollBody;
        [SerializeField] private Transform leftTransform;
        [SerializeField] private Transform rightTransform;

        private Vector3 _leftHitPoint;
        private Vector3 _rightHitPoint;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(_leftHitPoint, .5f);
            Gizmos.DrawSphere(_rightHitPoint, .5f);
        }

        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            CheckLeft();
            CheckRight();
            ExecuteHeight();
        }

        private void ExecuteHeight()
        {
            var targetDir = _leftHitPoint - _rightHitPoint;
            var angle = Vector3.Angle(root.right, targetDir);
            if (angle > 90f)
            {
                angle = 180f - angle;
            }

            rollBody.rotation = _leftHitPoint.y > _rightHitPoint.y
                ? Quaternion.Euler(rollBody.eulerAngles.x, rollBody.eulerAngles.y, -angle)
                : Quaternion.Euler(rollBody.eulerAngles.x, rollBody.eulerAngles.y, angle);
        }

        private void CheckLeft()
        {
            var leftPosition = leftTransform.position;
            leftPosition.y += 999f;
            var ray = new Ray(leftPosition, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
            {
                _leftHitPoint = hitInfo.point;
            }
        }

        private void CheckRight()
        {
            var rightPosition = rightTransform.position;
            rightPosition.y += 999f;
            var ray = new Ray(rightPosition, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
            {
                _rightHitPoint = hitInfo.point;
            }
        }
    }
}