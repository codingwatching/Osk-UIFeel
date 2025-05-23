using UnityEngine;
using DG.Tweening;

namespace OSK
{
    [DisallowMultipleComponent]
    public class PositionProvider : DoTweenBaseProvider
    {
        [HideInInspector] public Vector3 from = Vector3.zero;
        [HideInInspector] public Vector3 to = Vector3.zero;

        public override object GetStartValue() => from;
        public override object GetEndValue() => to;

        private Vector3 initialPosition;


        public bool isResetToFrom = false;
        public bool isLocal = true;

        public override void ProgressTween(bool isPlayBackwards)
        {
            initialPosition = transform.localPosition;

            if (isLocal)
                transform.localPosition = from;
            else
                transform.position = from;

            tweener = isLocal
                ? transform.DOLocalMove(to, settings.duration)
                : transform.DOMove(to, settings.duration);
            ;
            base.ProgressTween(isPlayBackwards);
        }

        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }

        public override void Stop()
        {
            base.Stop();
            transform.localPosition = initialPosition;
        }
    }
}