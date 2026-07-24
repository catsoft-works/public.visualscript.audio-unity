using System;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Play Sound Effect")]
    [Image(typeof(IconMusicNote), ColorTheme.Type.Yellow)]

    [Description("Plays an Audio resource on the Sound Effect channel")]
    [Category("Audio/Play Sound Effect")]

    [Serializable]
    public class InstructionAudioUnityPlaySoundEffect : InstructionAudioUnityPlayBase
    {
        protected sealed override AudioChannel Channel => AudioChannel.SoundEffect;

        protected sealed override AudioData CreateDefaultAudioData()
        {
            return new AudioData(false, 0f, TimeMode.GameTime, true, 1f, false);
        }
    }
}