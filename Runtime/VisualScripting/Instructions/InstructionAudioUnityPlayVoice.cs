using System;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Play Voice")]
    [Image(typeof(IconFace), ColorTheme.Type.Yellow)]

    [Description("Plays an Audio resource on the Voice channel")]
    [Category("Audio/Play Voice")]

    [Serializable]
    public class InstructionAudioUnityPlayVoice : InstructionAudioUnityPlayBase
    {
        protected sealed override AudioChannel Channel => AudioChannel.Voice;

        protected sealed override AudioData CreateDefaultAudioData()
        {
            return new AudioData(false, 0f, TimeMode.GameTime, false, 1f, true);
        }
    }
}