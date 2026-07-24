using System;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Play UI")]
    [Image(typeof(IconButton), ColorTheme.Type.Yellow)]

    [Description("Plays an Audio resource on the UI channel")]
    [Category("Audio/Play UI")]

    [Serializable]
    public class InstructionAudioUnityPlayUI : InstructionAudioUnityPlayBase
    {
        protected sealed override AudioChannel Channel => AudioChannel.UI;

        protected sealed override AudioData CreateDefaultAudioData()
        {
            return new AudioData(false, 0f, TimeMode.RealTime, false, 0f, false);
        }
    }
}