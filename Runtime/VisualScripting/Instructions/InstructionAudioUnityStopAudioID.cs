using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Stop Audio ID")]
    [Image(typeof(IconMusic), ColorTheme.Type.Red, typeof(BadgeID))]

    [Description("Stops an active audio by its unique ID")]
    [Category("Audio/Stop Audio ID")]

    [Serializable]
    public class InstructionAudioUnityStopAudioID : Instruction
    {
        [SerializeField] private GetString m_AudioID = new GetString();
        [SerializeField] private GetNumber m_Transition = new GetNumber(3f);
        [SerializeField] private bool m_Await;

        protected override string GetTitle(GameObject source)
        {
            string transition = MathUtils.IsZero(this.m_Transition.GetPreview(source))
                ? string.Empty
                : $" in {this.m_Transition} seconds";

            return $"Stop {this.m_AudioID}{transition}";
        }

        protected override async Awaitable Run(Args args)
        {
            string audioId = this.m_AudioID.Get(args);
            float transition = this.m_Transition.GetFloat(args);

            Awaitable awaitable = AudioManager.Stop(new Guid(audioId), transition);
            if (this.m_Await) await awaitable;
        }
    }
}