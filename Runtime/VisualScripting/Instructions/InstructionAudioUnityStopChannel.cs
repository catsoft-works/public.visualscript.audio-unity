using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Stop Audio in Channel")]
    [Image(typeof(IconMusic), ColorTheme.Type.Red, typeof(BadgeList))]

    [Description("Stops all active audio queues being played in the specified channel")]
    [Category("Audio/Stop Audio in Channel")]

    [Serializable]
    public class InstructionAudioUnityStopChannel : Instruction
    {
        [SerializeField] private AudioChannel m_Channel = AudioChannel.Music;
        [SerializeField] private GetNumber m_Transition = new GetNumber(3f);
        [SerializeField] private bool m_Await;

        protected override string GetTitle(GameObject source)
        {
            string channel = TextUtils.ThemeAsKeyword(TextUtils.Humanize(this.m_Channel));
            string transition = MathUtils.IsZero(this.m_Transition.GetPreview(source))
                ? string.Empty
                : $" in {this.m_Transition} seconds";

            return $"Stop {channel}{transition}";
        }

        protected override async Awaitable Run(Args args)
        {
            float transition = this.m_Transition.GetFloat(args);

            Awaitable awaitable = AudioManager.StopChannel(this.m_Channel, transition);
            if (this.m_Await) await awaitable;
        }
    }
}