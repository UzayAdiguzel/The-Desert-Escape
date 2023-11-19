using System;
using Unity.Mathematics;
using UnityEngine;

namespace MapSystem
{
    public static class MapMathHelper
    {
        public static float2 Size;

        public static Vector3 CalculateMyPosition(Vector3 current, Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    current = AddLeft(current);
                    break;
                case Direction.Right:
                    current = AddRight(current);
                    break;
                case Direction.Forward:
                    current = AddForward(current);
                    break;
                case Direction.Back:
                    current = AddBack(current);
                    break;
                case Direction.LeftForward:
                    current = AddLeft(current);
                    current = AddForward(current);
                    break;
                case Direction.LeftBack:
                    current = AddLeft(current);
                    current = AddBack(current);
                    break;
                case Direction.RightForward:
                    current = AddRight(current);
                    current = AddForward(current);
                    break;
                case Direction.RightBack:
                    current = AddRight(current);
                    current = AddBack(current);
                    break;
                case Direction.Center:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            return current;
        }
        
        private static Vector3 AddLeft(Vector3 current)
        {
            current.x -= Size.x;
            return current;
        }

        private static Vector3 AddRight(Vector3 current)
        {
            current.x += Size.x;
            return current;
        }

        private static Vector3 AddForward(Vector3 current)
        {
            current.z += Size.y;
            return current;
        }

        private static Vector3 AddBack(Vector3 current)
        {
            current.z -= Size.y;
            return current;
        }
    }
}