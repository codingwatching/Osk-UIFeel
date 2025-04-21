using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace OSK
{
    public class ButtonUI : BaseButton, IPointerDownHandler, IPointerUpHandler
    {
        [Space]
        [Header("Button UI")]
        [SerializeField] private Graphic m_Image;
        [SerializeField] private Color colorShow = Color.white;
        [SerializeField] private Color colorPress = new Color(0.83f, 0.83f, 0.83f, 1f);
        private bool isPress;


        public TweenSetting DownSetting = new TweenSetting(0.1f, false, AnimationCurve.Linear(0, 0, 1, 1), Ease.OutQuad,
            new Vector3(0.95f, 0.95f, 1), new Vector3(0,0, 7), 7, 10);

        public TweenSetting UpSetting = new TweenSetting(0.1f, false, AnimationCurve.Linear(0, 0, 1, 1), Ease.OutQuad,
            new Vector3(1.02f, 1.02f, 1), Vector3.zero, 3 , 1);

        [Space] [Header("Second Up Setting")]
        [Button("Second Up Setting")]
        public bool NeedSecondUpSetting = true;

        [ShowIf(nameof(NeedSecondUpSetting), true)]
        public TweenSetting SecondUpSetting = new TweenSetting(0.08f, false, AnimationCurve.Linear(0, 0, 1, 1),
            Ease.OutQuad, new Vector3(1, 1, 1), new Vector3(0,0, -5), 5 , 7);


        private void Awake()
        {
            if (m_Image == null)
                m_Image = gameObject.GetComponent<Image>();
        }

        private void OnEnable()
        {
            m_Image.color = colorShow;
            isPress = false;
            Recover();
        }

        public void Recover()
        {
            KillTween();
            ApplyTweenReset(DownSetting);
            ApplyTweenReset(UpSetting);
            ApplyTweenReset(SecondUpSetting);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isPress) return;
            isPress = true;
            m_Image.color = colorPress;
            OnPointerDownEvent?.Invoke();
            DownAnim();
            
            if (playSoundOnClick)
                PlaySound();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isPress) return;
            isPress = false;

            m_Image.color = colorShow;
            OnPointerUpEvent?.Invoke();
            UpAnim();
        }

        public void DownAnim()
        {
            AnimInternal(DownSetting);
        }

        public void UpAnim()
        {
            AnimInternal(UpSetting);
            if (NeedSecondUpSetting)
                mTween.OnComplete(() => { AnimInternal(SecondUpSetting); });
        }

        private void AnimInternal(TweenSetting setting)
        {
            KillTween();
            ApplyTweenDown(setting);
        }
    }
}