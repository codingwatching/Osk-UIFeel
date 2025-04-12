using UnityEngine;
using DG.Tweening;

namespace OSK
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupProvider : DoTweenBaseProvider
    {
        [HideInInspector] public CanvasGroup canvasGroup;
        [HideInInspector] public float from = 0;
        [HideInInspector] public float to = 1;
        
        private float initialAlpha;

        public override object GetStartValue() => from;
        public override object GetEndValue() => to;

        public override void ProgressTween(bool isPlayBackwards)
        {
            canvasGroup = gameObject.GetOrAdd<CanvasGroup>();
            initialAlpha = canvasGroup.alpha;
            
            canvasGroup.alpha = from;
            tweener = canvasGroup.DOFade(to, settings.duration);
            base.ProgressTween(isPlayBackwards);
        }
        
        
        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }


        public override void Stop()
        {
            base.Stop();
            canvasGroup.alpha = initialAlpha;
        }
 
    }
}