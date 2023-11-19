using UnityEngine;

namespace MapSystem
{
    public class PlebMapChanger : MonoBehaviour
    {
        [Sirenix.OdinInspector.Button]
        private void MoveLeft()
        {
           MapEvents.ChangeMap(Direction.Left);
        }
        
        [Sirenix.OdinInspector.Button]
        private void MoveRight()
        {
            MapEvents.ChangeMap(Direction.Right);
        }
        
        [Sirenix.OdinInspector.Button]
        private void MoveForward()
        {
            MapEvents.ChangeMap(Direction.Forward);
        }
        
        [Sirenix.OdinInspector.Button]
        private void MoveBack()
        {
            MapEvents.ChangeMap(Direction.Back);
        }
    }
}