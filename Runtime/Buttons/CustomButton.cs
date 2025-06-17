using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OSK
{
    [RequireComponent(typeof(Image))]
    public class CustomButton : Button, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Custom Events")] 
        public UnityEvent onPress;
        public UnityEvent onDown;
        public UnityEvent onRelease;
        public UnityEvent onPlaySound;
        

        private Coroutine holdCoroutine;
        public Transform _holdTransform;

        public Transform HoldTransform
        {
            get
            {
                if (_holdTransform == null)
                {
                    _holdTransform = transform;
                }

                return _holdTransform;
            }
            set => _holdTransform = value;
        }

        private Vector3 m_Scale;
        public float scaleTo = 0.9f;
        public bool enableAudio = false;
        public float timeToHold = 0;

        protected override void Awake()
        {
            base.Awake();
            m_Scale = HoldTransform.localScale;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            holdCoroutine = StartCoroutine(HoldRoutine());
            onDown?.Invoke();
            HoldTransform.localScale = m_Scale * scaleTo;

            if (enableAudio)
            {
                onPlaySound.Invoke();
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (holdCoroutine != null)
                StopCoroutine(holdCoroutine);
            onRelease?.Invoke();
            HoldTransform.localScale = m_Scale;
            timeToHold = 0f;
        }

        private IEnumerator HoldRoutine()
        {
            while (true)
            {
                onPress?.Invoke();
                timeToHold += Time.deltaTime;
                yield return null;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            HoldTransform.localScale = m_Scale;
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
            }
        }
    }
}