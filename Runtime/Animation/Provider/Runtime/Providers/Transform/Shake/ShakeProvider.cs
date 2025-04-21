using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;

namespace OSK
{
    public enum TypeShake
    {
        Position,
        Rotation,
        Scale
    }

    public class ShakeProvider : DoTweenBaseProvider
    {
        public TypeShake typeShake = TypeShake.Position;
        public bool isRandom = false;
        public Vector3 strength = new Vector3(1, 1, 1);
        public int vibrato = 10;
        public float randomness = 90;
        public bool snapping = false;
        public bool fadeOut = true;
 
        private Vector3 originalPosition = Vector3.zero;
        private Vector3 originalRotation = Vector3.zero;
        private Vector3 originalScale = Vector3.one;

        public override void OnEnable()
        {
            base.OnEnable();
            
            originalPosition = RootTransform.localPosition;
            originalRotation = RootTransform.localEulerAngles;
            originalScale = RootTransform.localScale;
        }


        public override object GetStartValue() => null;
        public override object GetEndValue() => null;
        
        

        public override void  ProgressTween(bool isPlayBackwards)
        {
            RootTransform.localPosition = originalPosition;
            RootTransform.localEulerAngles = originalRotation;
            RootTransform.localScale = originalScale;

            if (tweener == null)
            {
                originalPosition = RootTransform.localPosition;
                originalRotation = RootTransform.localEulerAngles;
                originalScale = RootTransform.localScale;
            }
            
            var rs = (isRandom) ? Extension.RandomVector3(-strength, strength) : strength;
            tweener = typeShake switch
            {
                TypeShake.Position => RootTransform.DOShakePosition(settings.duration, rs, vibrato, randomness, snapping,  fadeOut),
                TypeShake.Rotation => RootTransform.DOShakeRotation(settings.duration, rs, vibrato, randomness, fadeOut),
                TypeShake.Scale => RootTransform.DOShakeScale(settings.duration, rs, vibrato, randomness, fadeOut),
                _ => null
            };
            base.ProgressTween(isPlayBackwards);
        }


        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }

        public override void Stop()
        {
            base.Stop();
            tweener?.Rewind();
            tweener = null;

            RootTransform.localPosition = originalPosition;
            RootTransform.localEulerAngles = originalRotation;
            RootTransform.localScale = originalScale;
        }
    }
}