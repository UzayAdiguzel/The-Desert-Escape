using Helpers;
using UnityEngine;

namespace BoatSystem
{
    public class BoatDestroyablePoint : MonoBehaviour, IObject
    {
        public int InstanceID { get; set; }

        private void OnEnable()
        {
            InstanceID = gameObject.GetInstanceID();
            this.AddObj<BoatDestroyablePoint>();
        }

        private void OnDisable()
        {
            this.RemoveObj<BoatDestroyablePoint>();
        }
    }
}