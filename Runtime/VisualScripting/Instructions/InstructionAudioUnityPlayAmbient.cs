using System;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Play Ambient")]
    [Image(typeof(IconBird), ColorTheme.Type.Yellow)]

    [Description("Plays an Audio resource on the Ambient channel")]
    [Category("Audio/Play Ambient")]

    [Serializable]
    public class InstructionAudioUnityPlayAmbient : InstructionAudioUnityPlayBase
    {
        protected sealed override AudioChannel Channel => AudioChannel.Ambient;

        protected sealed override AudioData CreateDefaultAudioData()
        {
            return new AudioData(true, 3f, TimeMode.GameTime, false, 1f, true);
        }
    }
}