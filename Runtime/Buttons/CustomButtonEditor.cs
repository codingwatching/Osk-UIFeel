#if UNITY_EDITOR
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(OSK.CustomButton), true)]
    [CanEditMultipleObjects]
    /// <summary>
    ///   Custom Editor for the Button Component.
    ///   Extend this class to write a custom editor for a component derived from Button.
    /// </summary>
    public class CustomButtonEditor : SelectableEditor
    {
        SerializedProperty m_OnClickProperty;
        SerializedProperty m_TransformProperty;
        SerializedProperty m_ScaleProperty;
        
        SerializedProperty m_EnableAudioProperty;
        SerializedProperty m_AudioClickIDProperty;
        SerializedProperty m_TimeToHoldProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_OnClickProperty = serializedObject.FindProperty("m_OnClick");
            m_TransformProperty = serializedObject.FindProperty("_holdTransform");
            m_ScaleProperty = serializedObject.FindProperty("scaleTo");
            m_EnableAudioProperty = serializedObject.FindProperty("enableAudio");
            m_AudioClickIDProperty = serializedObject.FindProperty("audioClickID");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_OnClickProperty);
            EditorGUILayout.PropertyField(m_TransformProperty);
            
            EditorGUILayout.PropertyField(m_ScaleProperty);
            EditorGUILayout.PropertyField(m_EnableAudioProperty);
            if (m_EnableAudioProperty.boolValue)
            {
                EditorGUILayout.PropertyField(m_AudioClickIDProperty);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif