using DG.Tweening;
using UnityEngine;

namespace OSK
{
    public class GameObjectProvider : DoTweenBaseProvider
    {
        public GameObject gameObject;
        [HideInInspector] public bool from;
        [HideInInspector] public bool to;
        
        private bool initialActive;
        
        public override object GetStartValue() => from;
        public override object GetEndValue() => to;

        public override void ProgressTween(bool isPlayBackwards)
        {
            initialActive = gameObject.activeSelf;
            gameObject.SetActive(from);
            tweener = DOVirtual.Float(from ? 1 : 0, to ? 1 : 0, settings.duration,
                value => gameObject.SetActive(value > 0));
            base.ProgressTween(isPlayBackwards);
        }

       
        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }


        public override void Stop()
        {
            base.Stop();
            gameObject.SetActive(initialActive);
        }
    }
}