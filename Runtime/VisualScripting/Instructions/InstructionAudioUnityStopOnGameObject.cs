using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Stop Audio on Game Object")]
    [Image(typeof(IconMusic), ColorTheme.Type.Red, typeof(BadgeCubeSolid))]

    [Description("Stops any active audio queues associated with a particular Game Object")]
    [Category("Audio/Stop Audio on Game Object")]

    [Serializable]
    public class InstructionAudioUnityStopOnGameObject : Instruction
    {
        [SerializeField] private GetGameObject m_GameObject = new GetGameObject();
        [SerializeField] private GetNumber m_Transition = new GetNumber(3f);
        [SerializeField] private bool m_Await;

        protected override string GetTitle(GameObject source)
        {
            string transition = MathUtils.IsZero(this.m_Transition.GetPreview(source))
                ? string.Empty
                : $" in {this.m_Transition} seconds";

            return $"Stop Audio on {this.m_GameObject}{transition}";
        }

        protected override async Awaitable Run(Args args)
        {
            GameObject gameObject = this.m_GameObject.Get(args);
            float transition = this.m_Transition.GetFloat(args);

            Awaitable awaitable = AudioManager.Stop(gameObject, transition);
            if (this.m_Await) await awaitable;
        }
    }
}