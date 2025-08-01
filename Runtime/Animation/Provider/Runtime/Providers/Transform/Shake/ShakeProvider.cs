using DG.Tweening;
using UnityEditor;
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
        
        private ScaleProvider scaleProvider;
        private RotationProvider rotationProvider;
        private PositionProvider positionProvider;

        public override void OnEnable()
        {
            base.OnEnable();
            
            scaleProvider = GetComponent<ScaleProvider>();
            rotationProvider = GetComponent<RotationProvider>();
            positionProvider = GetComponent<PositionProvider>();

            switch (typeShake)
            {
                case TypeShake.Position:
                    if (positionProvider != null)
                    {
                        originalPosition = positionProvider.GetEndValue() as Vector3? ?? RootTransform.localPosition;
                    }
                    else
                    {
                        originalPosition = RootTransform.localPosition;
                    }
                    break;
                case TypeShake.Rotation:
                    if (rotationProvider != null)
                    {
                        originalRotation = rotationProvider.GetEndValue() as Vector3? ?? RootTransform.localEulerAngles;
                    }
                    else
                    {
                        originalRotation = RootTransform.localEulerAngles;
                    }
                    break;
                case TypeShake.Scale:
                    if (scaleProvider != null)
                    {
                        originalScale = scaleProvider.GetEndValue() as Vector3? ?? RootTransform.localScale;
                    }
                    else
                    {
                        originalScale = RootTransform.localScale;
                    }
                    break;
            }
        }


        public override object GetStartValue() => null;
        public override object GetEndValue() => null;


        public override void ProgressTween(bool isPlayBackwards)
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
                TypeShake.Position => RootTransform.DOShakePosition(settings.duration, rs, vibrato, randomness,
                    snapping, fadeOut),
                TypeShake.Rotation =>
                    RootTransform.DOShakeRotation(settings.duration, rs, vibrato, randomness, fadeOut),
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

            switch (typeShake)
            {
                case TypeShake.Position:
                    RootTransform.localPosition = originalPosition;
                    break;
                case TypeShake.Rotation:
                    RootTransform.localEulerAngles = originalRotation;
                    break;
                case TypeShake.Scale:
                    RootTransform.localScale = originalScale;
                    break;
            }
        }
    }
}