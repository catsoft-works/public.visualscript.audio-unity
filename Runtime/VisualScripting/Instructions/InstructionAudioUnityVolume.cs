using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Set Audio Channel Volume")]
    [Image(typeof(IconVolume), ColorTheme.Type.Yellow)]

    [Description("Changes the Volume of the specified channel")]
    [Category("Audio/Volume/Channel")]

    [Parameter("Channel", "The Audio Channel to change the volume of")]
    [Parameter("Volume", "The new volume value")]

    [Keywords("Set", "Change", "Modify")]

    [Serializable]
    public class InstructionAudioUnityVolume : Instruction
    {
        [SerializeField] private AudioChannel m_Channel = AudioChannel.Music;

        [SerializeField] private GetNumber m_Volume = new GetNumber(1f);
        [SerializeField] private TweenTransition m_Transition = new TweenTransition();

        [NonSerialized] private bool m_IsComplete;

        protected override string GetTitle(GameObject source)
        {
            string channel = TextUtils.Humanize(this.m_Channel);
            string result = $"{this.m_Volume}{this.m_Transition}";

            return $"Volume of {TextUtils.ThemeAsKeyword(channel)} = {result}";
        }

        protected override async Awaitable Run(Args args)
        {
            bool wait = this.m_Transition.Await;
            this.m_IsComplete = false;

            TweenManager.Run(new TweenStaticInput<float>(
                $"volume-{this.m_Channel}",
                VolumeManager.Get(this.m_Channel),
                this.m_Volume.GetFloat(args),
                this.OnUpdate,
                wait ? this.OnFinish : null,
                this.m_Transition.GetDuration(args),
                this.m_Transition.EaseMode,
                this.m_Transition.TimeMode
            ));

            if (wait) await this.Until(this.IsComplete);
        }

        private void OnUpdate(bool isAlive, float volume)
        {
            if (isAlive) VolumeManager.Set(this.m_Channel, volume);
        }

        private void OnFinish() => this.m_IsComplete = true;

        private bool IsComplete() => this.m_IsComplete;
    }
}