using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using VisualScript.Runtime.AudioUnity;

namespace VisualScript.Editor.AudioUnity
{
    [CustomPropertyDrawer(typeof(AudioUnityRepository))]
    public class AudioUnityRepositoryDrawer : PropertyDrawer
    {
        // PAINT METHODS: -------------------------------------------------------------------------

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            root.Add(new Title("Audio"));
            root.Add(Space.CreateSmall());

            root.Add(new Title("Audio Mixer Groups"));

            PropertyField soundEffects = new PropertyField(property.FindPropertyRelative("m_SoundEffects"));
            PropertyField ambient = new PropertyField(property.FindPropertyRelative("m_Ambient"));
            PropertyField music = new PropertyField(property.FindPropertyRelative("m_Music"));
            PropertyField voice = new PropertyField(property.FindPropertyRelative("m_Voice"));
            PropertyField ui = new PropertyField(property.FindPropertyRelative("m_UI"));

            soundEffects.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            ambient.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            music.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            voice.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            ui.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);

            root.Add(soundEffects);
            root.Add(ambient);
            root.Add(music);
            root.Add(voice);
            root.Add(ui);

            return root;
        }
    }
}