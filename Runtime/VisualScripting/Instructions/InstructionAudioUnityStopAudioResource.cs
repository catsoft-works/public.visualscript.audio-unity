using System;
using UnityEngine;
using UnityEngine.Audio;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Stop Audio Resource")]
    [Image(typeof(IconMusic), ColorTheme.Type.Red)]

    [Description("Stops all active audio queues that match the specified Audio Resource")]
    [Category("Audio/Stop Audio Resource")]

    [Keywords("Clip")]

    [Serializable]
    public class InstructionAudioUnityStopAudioResource : Instruction
    {
        [SerializeField] private GetAudioResource m_Audio = new GetAudioResource();
        [SerializeField] private GetNumber m_Transition = new GetNumber(3f);
        [SerializeField] private bool m_Await;

        protected override string GetTitle(GameObject source)
        {
            string transition = MathUtils.IsZero(this.m_Transition.GetPreview(source))
                ? string.Empty
                : $" in {this.m_Transition} seconds";

            return $"Stop {this.m_Audio}{transition}";
        }

        protected override async Awaitable Run(Args args)
        {
            AudioResource audioResource = this.m_Audio.Get(args);
            float transition = this.m_Transition.GetFloat(args);

            Awaitable awaitable = AudioManager.Stop(audioResource, transition);
            if (this.m_Await) await awaitable;
        }
    }
}