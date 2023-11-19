using UnityEngine;

namespace BoatSystem
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool rotateAround;
        [SerializeField] private float rotateSpeed = 120f;
        [SerializeField] private Vector3 axis = Vector3.up;
        [SerializeField] private Vector3 rotateAxis = Vector3.up;
        private Camera _camera;

        private void LateUpdate()
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }

            transform.LookAt(_camera.transform.position, axis);

            if (rotateAround)
            {
                transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
            }
        }
    }
}