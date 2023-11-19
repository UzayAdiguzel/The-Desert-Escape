using System;
using BoatSystem;
using Helpers;
using UnityEngine;

namespace WormSystem
{
    public class WormController : MonoBehaviour
    {
        [SerializeField] private float speed;

        private BoatDestroyablePoint _myTarget;
        private ObjectListTracker<BoatDestroyablePoint> _targetList;

        private void Awake()
        {
            _targetList = new ObjectListTracker<BoatDestroyablePoint>();
            InvokeRepeating(nameof(FindTarget), 0f, 1f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Update()
        {
            Move();
        }

        private void FindTarget()
        {
            _targetList.SyncList();

            foreach (var boatDestroyablePoint in _targetList.CurrentList)
            {
                if (_myTarget == null)
                {
                    _myTarget = boatDestroyablePoint;
                    continue;
                }

                var distance = Vector3.Distance(transform.position, boatDestroyablePoint.transform.position);
                var currentDistance = Vector3.Distance(transform.position, _myTarget.transform.position);
                if (distance < currentDistance) _myTarget = boatDestroyablePoint;
            }
        }

        private void Move()
        {
            if (_myTarget == null) return;
            var direction = (_myTarget.transform.position - transform.position).normalized;
            direction.y = 0f;
            transform.position += direction * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
            var currentDistance = Vector3.Distance(transform.position, _myTarget.transform.position);
            if (currentDistance > 50f) Destroy(gameObject);
        }
    }
}