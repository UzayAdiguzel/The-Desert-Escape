using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace MapSystem
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private float2 size;
        [SerializeField] private MapPoint pointBp;
        
        

        private readonly Dictionary<Direction, MapPointData> _directionToPointDict = new Dictionary<Direction, MapPointData>();

        private void Awake()
        {
            MapMathHelper.Size = size;
            
            var centerPos = center.position;
            _directionToPointDict.Add(Direction.Center, new MapPointData(centerPos, Direction.Center, CreatePoint()));
            _directionToPointDict.Add(Direction.Left, new MapPointData(centerPos, Direction.Left, CreatePoint()));
            _directionToPointDict.Add(Direction.Right, new MapPointData(centerPos, Direction.Right, CreatePoint()));
            _directionToPointDict.Add(Direction.Forward, new MapPointData(centerPos, Direction.Forward, CreatePoint()));
            _directionToPointDict.Add(Direction.Back, new MapPointData(centerPos, Direction.Back, CreatePoint()));
            _directionToPointDict.Add(Direction.LeftForward, new MapPointData(centerPos, Direction.LeftForward, CreatePoint()));
            _directionToPointDict.Add(Direction.LeftBack, new MapPointData(centerPos, Direction.LeftBack, CreatePoint()));
            _directionToPointDict.Add(Direction.RightForward, new MapPointData(centerPos, Direction.RightForward, CreatePoint()));
            _directionToPointDict.Add(Direction.RightBack, new MapPointData(centerPos, Direction.RightBack, CreatePoint()));
            
            MapEvents.OnChangeMapPoint += OnChangeMapPoint;
        }

        private void OnDisable()
        {
            MapEvents.OnChangeMapPoint -= OnChangeMapPoint;
        }

        private void OnChangeMapPoint(Direction obj)
        {

            if (obj == Direction.Left)
                MoveLeft();
            else if (obj == Direction.Right)
                MoveRight();
            else if (obj == Direction.Forward)
                MoveForward();
            else if (obj == Direction.Back)
                MoveBack();
        }

        private void MoveLeft()
        {
            // Right is Dead
            var oldRight = _directionToPointDict[Direction.Right];
            oldRight.DestroyYourself();
            var oldRightForward = _directionToPointDict[Direction.RightForward];
            oldRightForward.DestroyYourself();
            var oldRightBack = _directionToPointDict[Direction.RightBack];
            oldRightBack.DestroyYourself();
            
            
            var oldLeftPoint = _directionToPointDict[Direction.Left];
            var newCenterPos = oldLeftPoint.CurrentPos;
            center.position = newCenterPos;
            
            var oldCenter = _directionToPointDict[Direction.Center];//New Right
            _directionToPointDict[Direction.Right] = oldCenter;
            oldCenter.ChangeDirection(Direction.Right);

            var oldForward = _directionToPointDict[Direction.Forward];//new RF
            _directionToPointDict[Direction.RightForward] = oldForward;
            oldForward.ChangeDirection(Direction.RightForward);

            var oldBack = _directionToPointDict[Direction.Back];//new RB
            _directionToPointDict[Direction.RightBack] = oldBack;
            oldBack.ChangeDirection(Direction.RightBack);

            var oldLeftForward = _directionToPointDict[Direction.LeftForward]; // New F
            _directionToPointDict[Direction.Forward] = oldLeftForward;
            oldLeftForward.ChangeDirection(Direction.Forward);

            var oldLeftBack = _directionToPointDict[Direction.LeftBack]; //New B
            _directionToPointDict[Direction.Back] = oldLeftBack;
            oldLeftBack.ChangeDirection(Direction.Back);

            _directionToPointDict[Direction.Left] = new MapPointData(newCenterPos, Direction.Left, CreatePoint());
            _directionToPointDict[Direction.LeftForward] = new MapPointData(newCenterPos, Direction.LeftForward, CreatePoint());
            _directionToPointDict[Direction.LeftBack] = new MapPointData(newCenterPos, Direction.LeftBack, CreatePoint());
            _directionToPointDict[Direction.Center] = oldLeftPoint;
            oldLeftPoint.ChangeDirection(Direction.Center);
        }
        
        private void MoveRight()
        {
            //Left is dead
            var oldLeft = _directionToPointDict[Direction.Left];
            oldLeft.DestroyYourself();
            var oldLeftForward = _directionToPointDict[Direction.LeftForward];
            oldLeftForward.DestroyYourself();
            var oldLeftBack = _directionToPointDict[Direction.LeftBack];
            oldLeftBack.DestroyYourself();
            
            var oldRight = _directionToPointDict[Direction.Right];
            var newCenterPos = oldRight.CurrentPos;
            center.position = newCenterPos;
            
            var oldCenter = _directionToPointDict[Direction.Center];//New Left
            _directionToPointDict[Direction.Left] = oldCenter;
            oldCenter.ChangeDirection(Direction.Left);

            var oldForward = _directionToPointDict[Direction.Forward];//new LF
            _directionToPointDict[Direction.LeftForward] = oldForward;
            oldForward.ChangeDirection(Direction.LeftForward);

            var oldBack = _directionToPointDict[Direction.Back];//new LB
            _directionToPointDict[Direction.LeftBack] = oldBack;
            oldBack.ChangeDirection(Direction.LeftBack);

            var oldRightForward = _directionToPointDict[Direction.RightForward]; // New F
            _directionToPointDict[Direction.Forward] = oldRightForward;
            oldRightForward.ChangeDirection(Direction.Forward);

            var oldRightBack = _directionToPointDict[Direction.RightBack]; //New B
            _directionToPointDict[Direction.Back] = oldRightBack;
            oldRightBack.ChangeDirection(Direction.Back);

            
            _directionToPointDict[Direction.Right] = new MapPointData(newCenterPos, Direction.Right, CreatePoint());
            _directionToPointDict[Direction.RightForward] = new MapPointData(newCenterPos, Direction.RightForward, CreatePoint());
            _directionToPointDict[Direction.RightBack] = new MapPointData(newCenterPos, Direction.RightBack, CreatePoint());
            _directionToPointDict[Direction.Center] = oldRight;
            oldRight.ChangeDirection(Direction.Center);
        }
        
        private void MoveForward()
        {
            //back is dead
            var oldBack = _directionToPointDict[Direction.Back];
            oldBack.DestroyYourself();
            var oldLeftBack = _directionToPointDict[Direction.LeftBack];
            oldLeftBack.DestroyYourself();
            var oldRightBack = _directionToPointDict[Direction.RightBack];
            oldRightBack.DestroyYourself();
            
            var oldForward = _directionToPointDict[Direction.Forward];
            var newCenterPos = oldForward.CurrentPos;
            center.position = newCenterPos;
            
            var oldCenter = _directionToPointDict[Direction.Center];//New Back
            _directionToPointDict[Direction.Back] = oldCenter;
            oldCenter.ChangeDirection(Direction.Back);

            var oldRight = _directionToPointDict[Direction.Right];//new RB
            _directionToPointDict[Direction.RightBack] = oldRight;
            oldRight.ChangeDirection(Direction.RightBack);

            var oldLeft = _directionToPointDict[Direction.Left];//new LB
            _directionToPointDict[Direction.LeftBack] = oldLeft;
            oldLeft.ChangeDirection(Direction.LeftBack);

            var oldRightForward = _directionToPointDict[Direction.RightForward]; // New R
            _directionToPointDict[Direction.Right] = oldRightForward;
            oldRightForward.ChangeDirection(Direction.Right);

            var oldLeftForward = _directionToPointDict[Direction.LeftForward]; //New L
            _directionToPointDict[Direction.Left] = oldLeftForward;
            oldLeftForward.ChangeDirection(Direction.Left);

            _directionToPointDict[Direction.Forward] = new MapPointData(newCenterPos, Direction.Forward, CreatePoint());
            _directionToPointDict[Direction.LeftForward] = new MapPointData(newCenterPos, Direction.LeftForward, CreatePoint());
            _directionToPointDict[Direction.RightForward] = new MapPointData(newCenterPos, Direction.RightForward, CreatePoint());
            _directionToPointDict[Direction.Center] = oldForward;
            oldForward.ChangeDirection(Direction.Center);
        }
        
        private void MoveBack()
        {
            //Forward is dead
            var oldForward = _directionToPointDict[Direction.Forward];
            oldForward.DestroyYourself();
            var oldLeftForward = _directionToPointDict[Direction.LeftForward];
            oldLeftForward.DestroyYourself();
            var oldRightForward = _directionToPointDict[Direction.RightForward];
            oldRightForward.DestroyYourself();
            
            var oldBack = _directionToPointDict[Direction.Back];
            var newCenterPos = oldBack.CurrentPos;
            center.position = newCenterPos;
            
            var oldCenter = _directionToPointDict[Direction.Center];//New Forward
            _directionToPointDict[Direction.Forward] = oldCenter;
            oldCenter.ChangeDirection(Direction.Forward);

            var oldRight = _directionToPointDict[Direction.Right];//new RF
            _directionToPointDict[Direction.RightForward] = oldRight;
            oldRight.ChangeDirection(Direction.RightForward);

            var oldLeft = _directionToPointDict[Direction.Left];//new LF
            _directionToPointDict[Direction.LeftForward] = oldLeft;
            oldLeft.ChangeDirection(Direction.LeftForward);

            var oldRightBack = _directionToPointDict[Direction.RightBack]; // New R
            _directionToPointDict[Direction.Right] = oldRightBack;
            oldRightBack.ChangeDirection(Direction.Right);

            var oldLeftBack = _directionToPointDict[Direction.LeftBack]; //New L
            _directionToPointDict[Direction.Left] = oldLeftBack;
            oldLeftBack.ChangeDirection(Direction.Left);

            _directionToPointDict[Direction.Back] = new MapPointData(newCenterPos, Direction.Back, CreatePoint());
            _directionToPointDict[Direction.LeftBack] = new MapPointData(newCenterPos, Direction.LeftBack, CreatePoint());
            _directionToPointDict[Direction.RightBack] = new MapPointData(newCenterPos, Direction.RightBack, CreatePoint());
            _directionToPointDict[Direction.Center] = oldBack;
            oldBack.ChangeDirection(Direction.Center);
        }


        private MapPoint CreatePoint()
        {
            return Instantiate(pointBp, new Vector3(999f,-999f,999f), Quaternion.identity, transform);
        }

       


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var kvp in _directionToPointDict)
            {
                UnityEditor.Handles.Label(kvp.Value.CurrentPos + new Vector3(0f, 0f, 2f), kvp.Key.ToString());
                Gizmos.DrawSphere(kvp.Value.CurrentPos , 1f);
            }
        }

#endif
    }
}
