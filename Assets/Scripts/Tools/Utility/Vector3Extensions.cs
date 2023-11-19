using UnityEngine;

namespace Tools.Utility
{
    public static class Vector3Extensions
    {
        private const float CurveMult = 4f;

        public static Vector3 SmoothCurveX(Vector3 start, Vector3 end, float height, float t)
        {
            float Func(float x) => CurveMult * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(Func(t) + Mathf.Lerp(start.x, end.x, t), mid.y, mid.z);
        }
        
        public static Vector3 SmoothCurveY(Vector3 start, Vector3 end, float height, float t)
        {
            float Func(float x) => CurveMult * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }

        public static Vector3 SmoothCurveZ(Vector3 start, Vector3 end, float height, float t)
        {
            float Func(float x) => CurveMult * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, mid.y, Func(t) + Mathf.Lerp(start.z, end.z, t));
        }
    }
}