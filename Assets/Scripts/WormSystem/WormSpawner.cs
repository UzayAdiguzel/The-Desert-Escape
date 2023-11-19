using System;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WormSystem
{
    public class WormSpawner : MonoBehaviour
    {
        [SerializeField] private WormController bp;
        [SerializeField, Range(50f, 300f)] private float range;
        [SerializeField] private float rate;

        private float _counter;
        private ObjectListTracker<WormController> _wormList;

        private void Awake()
        {
            _wormList = new ObjectListTracker<WormController>();
        }

        private void Update()
        {
            _counter += Time.deltaTime;

            if (_counter > rate)
            {
                _counter = 0f;
                _wormList.SyncList();
                if (_wormList.CurrentList.Count > 100f) return;
                var position = Random.insideUnitSphere * Random.Range(10f, range);
                Instantiate(bp, transform.TransformPoint(position), Quaternion.identity,
                    ObjectParent.Instance.transform);
            }
        }
    }
}