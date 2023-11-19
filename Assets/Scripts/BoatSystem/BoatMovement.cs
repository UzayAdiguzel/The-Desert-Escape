using System;
using BasicBuildSystem;
using UnityEngine;

namespace BoatSystem
{
    public class BoatMovement : MonoBehaviour
    {
        [SerializeField] private Transform boatRoot;
        [SerializeField] private RudderRotationController rotationController;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float speed;
        [SerializeField] private float levelPerSpeed = 1f;
        

        private Vector3 _rotateAxis = Vector3.up;
        private float _defaultSpeed;

        private void Awake()
        {
            _defaultSpeed = speed;
            BuildEvents.OnChangeBuildLevel += ChangeBuildLevel;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _rotateAxis.y = rotationController.RudderDirection;
            boatRoot.position += boatRoot.forward * speed * Time.deltaTime;
            if (speed == 0) return;
            boatRoot.eulerAngles += _rotateAxis * rotationSpeed * Time.deltaTime;
        }

        private void ChangeBuildLevel(int obj)
        {
            speed = _defaultSpeed + obj * levelPerSpeed;
        }
    }
}