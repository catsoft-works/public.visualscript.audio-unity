using System;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Play Music")]
    [Image(typeof(IconHeadset), ColorTheme.Type.Yellow)]

    [Description("Plays an Audio resource on the Music channel")]
    [Category("Audio/Play Music")]

    [Serializable]
    public class InstructionAudioUnityPlayMusic : InstructionAudioUnityPlayBase
    {
        protected sealed override AudioChannel Channel => AudioChannel.Music;

        protected sealed override AudioData CreateDefaultAudioData()
        {
            return new AudioData(true, 3f, TimeMode.RealTime, false, 0f, false);
        }
    }
}