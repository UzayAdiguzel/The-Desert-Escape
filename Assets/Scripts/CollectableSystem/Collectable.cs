using System;
using BasicBuildSystem;
using DG.Tweening;
using Helpers;
using UnityEngine;

namespace CollectableSystem
{
    public class Collectable : MonoBehaviour, IObject
    {
        [SerializeField] private float exp = 1f;
        
        
        public int InstanceID { get; set; }

        private void Awake()
        {
            var pos = transform.position;
            pos.y -= 5f;
            transform.position = pos;
            pos.y += 5f;
            transform.DOMove(pos, 1f).SetEase(Ease.OutBack);
            
            InstanceID = gameObject.GetInstanceID();
            this.AddObj<Collectable>();
        }

        public void Collect()
        {
            this.RemoveObj<Collectable>();
            BuildEvents.AddExp(exp);
        }

        public void DestroyYourself()
        {
            this.RemoveObj<Collectable>();
            Destroy(gameObject);
        }
    }
}
