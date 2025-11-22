using System;
using UnityEngine;
using DG.Tweening;

namespace OSK
{
    public class RectPositionProvider : DoTweenBaseProvider
    {
        [HideInInspector] public Vector3 from = Vector3.zero;
        [HideInInspector] public Vector3 to = Vector3.zero;
        
        private Vector3 initialPosition;

        public override object GetStartValue() => from;
        public override object GetEndValue() => to;

        public override void ProgressTween(bool isPlayBackwards)
        {
            initialPosition = RootRectTransform.anchoredPosition;
            Vector3 realFrom = GetAdjustedPosition(from);
            Vector3 realTo = GetAdjustedPosition(to);

            RootRectTransform.anchoredPosition = realFrom;
            tweener = RootRectTransform.DOAnchorPos(realTo, settings.duration);
            base.ProgressTween(isPlayBackwards);
        }
        
        private Vector3 GetAdjustedPosition(Vector3 basePos)
        {
            if (RootRectTransform == null) return basePos;
            Vector2 currentPivot = RootRectTransform.pivot;
            Vector2 standardPivot = new Vector2(0.5f, 0.5f); 
            Vector2 pivotDelta = currentPivot - standardPivot;
            Vector2 size = RootRectTransform.rect.size;
            float offsetX = pivotDelta.x * size.x;
            float offsetY = pivotDelta.y * size.y;
            return new Vector3(basePos.x + offsetX, basePos.y + offsetY, basePos.z);
        }

        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }

        public override void Stop()
        {
            base.Stop();
            RootRectTransform.anchoredPosition = initialPosition;
        }
    }
}