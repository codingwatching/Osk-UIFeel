using UnityEngine;
using DG.Tweening;

namespace OSK
{
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public class RectDeltaSizeProvider : DoTweenBaseProvider
    {
        [HideInInspector] public Vector2 to = Vector2.zero;
        [HideInInspector] public Vector2 from = Vector2.zero;
        
        private Vector2 initialSize;
        
        public override object GetStartValue() => from;
        public override object GetEndValue() => to;
        
        public override void ProgressTween(bool isPlayBackwards)
        {
            if (RootRectTransform == null) return;
            initialSize = RootRectTransform.sizeDelta;

            Vector2 startValue = isPlayBackwards ? to : from;
            Vector2 endValue = isPlayBackwards ? from : to;
            RootRectTransform.sizeDelta = startValue;
            
            target = RootRectTransform; 
            tweener = RootRectTransform.DOSizeDelta(endValue, settings.duration)
                .SetUpdate(settings.updateType)
                .SetEase(settings.ease);

            base.ProgressTween(isPlayBackwards);
        }
 
        public override void Stop()
        {
            base.Stop();
            RootRectTransform.sizeDelta = initialSize;
        }
    }
}