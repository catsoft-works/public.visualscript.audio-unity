using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using VisualScript.Runtime.AudioUnity;

namespace VisualScript.Editor.AudioUnity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AudioSourceVolume))]

    public class AudioSourceVolumeEditor : UnityEditor.Editor
    {
        [NonSerialized] private ProgressBar m_Volume;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            SerializedProperty channel = this.serializedObject.FindProperty("m_Channel");
            PropertyField fieldChannel = new PropertyField(channel);

            root.Add(Space.CreateSmaller());
            root.Add(fieldChannel);

            return root;
        }
    }
}