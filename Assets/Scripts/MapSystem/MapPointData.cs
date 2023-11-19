using UnityEngine;

namespace MapSystem
{
    public struct MapPointData
    {
        private Vector3 _center;
        public Vector3 CurrentPos { get; }
        public MapPoint CurrentPoint { get; }

        public MapPointData(Vector3 center, Direction direction, MapPoint point)
        {
            _center = center;
            CurrentPos = MapMathHelper.CalculateMyPosition(center, direction);
            CurrentPoint = point;
            CurrentPoint.Initialize(CurrentPos, direction);
        }

        public void ChangeDirection(Direction direction)
        {
            CurrentPoint.ChangeDirection(direction);
        }

        public void DestroyYourself()
        {
            CurrentPoint.DestroyYourself();
        }

    }
}