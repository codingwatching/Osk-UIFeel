using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace OSK
{
    [DisallowMultipleComponent]
    public class RotationProvider : DoTweenBaseProvider
    {
        public bool isLocal = true;
        public RotateMode rotateMode = RotateMode.Fast;

        [HideInInspector]
        public Vector3 from = Vector3.zero;
        [HideInInspector]
        public Vector3 to = Vector3.zero;
        
        private Vector3 initialRotation;
        
        public override void OnEnable()
        {
            base.OnEnable();
            initialRotation = isLocal ? RootTransform.localEulerAngles : RootTransform.eulerAngles;
        }
        
        public override object GetStartValue() => from;
        public override object GetEndValue() => to;

        public override void ProgressTween(bool isPlayBackwards)
        { 
            RootTransform.localEulerAngles = from;
            tweener = isLocal
                ? RootTransform.DOLocalRotate(to, settings.duration, rotateMode)
                : RootTransform.DORotate(to, settings.duration, rotateMode);
            
            base.ProgressTween(isPlayBackwards);
        }

 
        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }

        public override void Stop()
        {
            base.Stop(); 
            
            if (isLocal)
                RootTransform.localEulerAngles = initialRotation;
            else
                RootTransform.eulerAngles = initialRotation;
        }
    }
}