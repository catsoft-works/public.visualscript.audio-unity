using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using VisualScript.Icons;
using VisualScript.Runtime;
using VisualScript.Runtime.AudioUnity;

namespace VisualScript.Editor.AudioUnity
{
    [CustomPropertyDrawer(typeof(AudioData))]
    public class AudioDataDrawer : PropertyDrawer
    {
        private static readonly IIcon ICON_AUDIO_SOURCE = new IconVolume(ColorTheme.Type.TextLight);

        private static readonly IIcon ICON_3D_SETTINGS = new IconVector3(ColorTheme.Type.TextLight);

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            root.Add(new PropertyField(property.FindPropertyRelative("m_Loop")));
            root.Add(new PropertyField(property.FindPropertyRelative("m_Transition")));

            Section sectionAudioSource = new Section(ICON_AUDIO_SOURCE.Texture, "Audio Settings", true);
            root.Add(Space.CreateSmaller());
            root.Add(sectionAudioSource);

            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_TimeScale")));
            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_Priority")));
            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_Volume")));
            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_Pitch")));
            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_StereoPan")));
            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_SpatialBlend")));
            sectionAudioSource.Add(new PropertyField(property.FindPropertyRelative("m_ReverbZoneMix")));

            Section section3DSettings = new Section(ICON_3D_SETTINGS.Texture, "Stereo Settings", true);
            root.Add(Space.CreateSmaller());
            root.Add(section3DSettings);

            section3DSettings.Add(new PropertyField(property.FindPropertyRelative("m_VolumeRolloff")));
            section3DSettings.Add(new PropertyField(property.FindPropertyRelative("m_MinDistance")));
            section3DSettings.Add(new PropertyField(property.FindPropertyRelative("m_MaxDistance")));
            section3DSettings.Add(Space.CreateSmaller());
            section3DSettings.Add(new PropertyField(property.FindPropertyRelative("m_DopplerLevel")));
            section3DSettings.Add(new PropertyField(property.FindPropertyRelative("m_Spread")));

            SerializedProperty location = property.FindPropertyRelative("m_Location");
            SerializedProperty position = property.FindPropertyRelative("m_Position");
            SerializedProperty transform = property.FindPropertyRelative("m_Transform");

            PropertyField fieldLocation = new PropertyField(location);

            Indent indentLocation = new Indent();
            PropertyField fieldPosition = new PropertyField(position);
            PropertyField fieldTransform = new PropertyField(transform);

            root.Add(Space.CreateSmaller());
            root.Add(fieldLocation);
            root.Add(indentLocation);
            indentLocation.Add(fieldPosition);
            indentLocation.Add(fieldTransform);

            fieldPosition.style.display = location.enumValueIndex == 2
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            fieldTransform.style.display = location.enumValueIndex == 3
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            fieldLocation.RegisterValueChangeCallback(changeEvent =>
            {
                fieldPosition.style.display = changeEvent.changedProperty.enumValueIndex == 2
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;

                fieldTransform.style.display = changeEvent.changedProperty.enumValueIndex == 3
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
            });

            root.Add(Space.CreateSmaller());
            root.Add(new PropertyField(property.FindPropertyRelative("m_HasTarget")));

            return root;
        }
    }
}