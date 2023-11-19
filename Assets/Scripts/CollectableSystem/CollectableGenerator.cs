using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CollectableSystem
{
    public class CollectableGenerator : MonoBehaviour
    {
        [SerializeField] private List<Collectable> collectableBpList;
        [SerializeField] private List<Transform> spawnPointList;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask collectableLayer;
        [SerializeField] private CollectableDetector detector;
        [SerializeField] private int activeObjCount = 300;
        
        


        private float _spawnTimer;
        private readonly float _spawnThreshold = 5f;
        private bool _initialize;
        private ObjectListTracker<Collectable> _collectableTracker;

        private IEnumerator Start()
        {
            _collectableTracker = new ObjectListTracker<Collectable>();
            yield return new WaitForSeconds(1);
            _initialize = true;
            Spawn();
            detector.TriggerExit.AddListener(TriggerExit);
        }

        private void Update()
        {
            if (!_initialize)
                return;

            _spawnTimer += Time.deltaTime;
            if (_spawnTimer < _spawnThreshold)
                return;

            _collectableTracker.SyncList();
            if (_collectableTracker.PureCurrentList.Count >= activeObjCount)
                return;

            _spawnTimer = 0f;
            Spawn();
        }

        private void TriggerExit(Collectable collectable)
        {
            collectable.DestroyYourself();
        }
        
        private void Spawn()
        {
            foreach (var parent in spawnPointList)
            {
                var collectable = collectableBpList.RandomItem();

                var raycastPoint = parent.position;
                raycastPoint.x += Random.Range(-5f, 5f);
                raycastPoint.z += Random.Range(-5f, 5f);
                raycastPoint.y += 1000f;

                var ray = new Ray(raycastPoint, Vector3.down);
                if (!Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundLayer))
                    continue;
                
                if (Physics.Raycast(ray, out var hitInfoCollectable, Mathf.Infinity, collectableLayer))
                    continue;

                var hitZero = hitInfo.point;
                var objParent = ObjectParent.Instance ? ObjectParent.Instance.transform : null;
                var instance = Instantiate(collectable, hitZero + new Vector3(0f, 0.5f, 0f), Quaternion.identity, objParent);
            }
        }
    }
}
