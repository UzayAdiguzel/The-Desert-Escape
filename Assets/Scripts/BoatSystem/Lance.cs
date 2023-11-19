using System;
using System.Collections.Generic;
using CollectableSystem;
using DG.Tweening;
using UnityEngine;

namespace BoatSystem
{
    public class Lance : MonoBehaviour
    {
        [SerializeField] private CollectableDetector collectableDetector;

        public bool LanceReady => !_launchSequence.IsActive();
        private Sequence _launchSequence;
        private List<Collectable> _currentCollectableList = new();

        private void Awake()
        {
            if (collectableDetector)
                collectableDetector.TriggerEnter.AddListener(FireOnCollectableEnter);
        }

        private void FireOnCollectableEnter(Collectable collectable)
        {
            if (LanceReady) return;
            collectable.transform.SetParent(transform);
            _currentCollectableList.Add(collectable);
        }

        public void Launch(Vector3 defaultPosition, Vector3 hitPosition, float duration)
        {
            _launchSequence = DOTween.Sequence();
            _launchSequence.Join(transform.DOMove(hitPosition, duration).SetEase(Ease.Linear));
            _launchSequence.Append(transform.DOLocalMove(defaultPosition, duration / 2f));
            _launchSequence.OnComplete(CheckAnyCollectedCollectable);
        }

        private void CheckAnyCollectedCollectable()
        {
            for (var index = _currentCollectableList.Count - 1; index >= 0; index--)
            {
                var collectable = _currentCollectableList[index];
                collectable.Collect();
                collectable.gameObject.SetActive(false);
                _currentCollectableList.Remove(collectable);
            }
        }
    }
}