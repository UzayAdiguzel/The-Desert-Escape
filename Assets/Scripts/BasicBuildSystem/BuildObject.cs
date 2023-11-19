using DG.Tweening;
using Tools.Utility;
using UnityEngine;

namespace BasicBuildSystem
{
    public class BuildObject : MonoBehaviour
    {
        public void Initialize(Transform target)
        {
            transform.localScale = Vector3.one * .05f;
            transform.SetParent(target.parent);
            var beginPos = transform.localPosition;
            transform.DOScale(Vector3.one, 1f);
            DOVirtual.Float(0f, 1f, 1f, value =>
            {
                transform.localPosition = Vector3Extensions.SmoothCurveY(beginPos, target.localPosition, 3f, value);
            }).OnComplete(() =>
            {
                transform.SetParent(target);
            });
        }
    }
}
