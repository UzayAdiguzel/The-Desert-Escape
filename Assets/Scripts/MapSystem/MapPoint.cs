using MapSystem.Detection;
using UnityEngine;

namespace MapSystem
{
    public class MapPoint : MonoBehaviour
    {
        [SerializeField] private MapActorDetector mapActorDetector;
        
        
        private Direction _myDirection;

        public void Initialize(Vector3 myPos, Direction targetDir)
        {
            transform.position = myPos;
            _myDirection = targetDir;
            
            mapActorDetector.TriggerEnter.AddListener(ActorEnter);
        }

        public void DestroyYourself()
        {
            mapActorDetector.TriggerEnter.RemoveListener(ActorEnter);
            Destroy(gameObject);
        }
        
        public void ChangeDirection(Direction targetDir)
        {
            _myDirection = targetDir;
        }

        private void ActorEnter(MapActor arg0)
        {
            MapEvents.ChangeMap(_myDirection);
        }
    }
}