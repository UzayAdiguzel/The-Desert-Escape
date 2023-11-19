using System;

namespace MapSystem
{
    public static class MapEvents
    {
        public static Action<Direction> OnChangeMapPoint;

        public static void ChangeMap(Direction newCenter)
        {
            OnChangeMapPoint?.Invoke(newCenter);
        }
    }
}