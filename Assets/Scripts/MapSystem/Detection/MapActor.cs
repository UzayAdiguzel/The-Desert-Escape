using System;
using Helpers;
using UnityEngine;

namespace MapSystem.Detection
{
    public class MapActor : MonoBehaviour, IObject
    {
        public int InstanceID { get; set; }

        private void OnEnable()
        {
            InstanceID = gameObject.GetInstanceID();
            this.AddObj<MapActor>();
        }

        private void OnDisable()
        {
            this.RemoveObj<MapActor>();
        }
    }
}