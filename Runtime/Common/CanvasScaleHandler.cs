using UnityEngine;
using UnityEngine.UI;

namespace OSK
{
    public class CanvasScaleHandler : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Canvas canvas;
        [SerializeField] private bool isPortrait;

        private void Awake()
        { 
            float newRatio = (float)Screen.width / Screen.height;

            if (camera == null)
                camera = GetComponent<Camera>();
 
            SetupCanvasScaler(newRatio);
        }
 

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying) 
                return;
            if (camera != null && canvas != null)
            { 
                float newRatio = (float)camera.pixelWidth / camera.pixelHeight;
                SetupCanvasScaler(newRatio);
                // float currentRatio = isPortrait ? 1080f / 1920 : 1920f / 1080;
                // float scaleFactor = canvasScaler.referenceResolution.x / camera.pixelWidth;
                // Logg.Log("canvas Scaler Factor X: " + scaleFactor);
            }
        }
#endif


        private void SetupCanvasScaler(float ratio)
        {
            canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = ratio > 0.65f ? 1 : 0;
        }
    }
}