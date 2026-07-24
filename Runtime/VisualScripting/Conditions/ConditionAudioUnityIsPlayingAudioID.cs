using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Is Playing Audio ID")]
    [Image(typeof(IconMusic), ColorTheme.Type.Yellow, typeof(BadgeID))]

    [Description("Returns true if the Audio ID is valid and currently being played")]
    [Category("Audio/Is Playing Audio ID")]

    [Parameter("Audio ID", "The Audio ID to check")]

    [Keywords("Current", "Now")]

    [Serializable]
    public class ConditionAudioUnityIsPlayingAudioID : Condition
    {
        [SerializeField] private GetString m_AudioID = new GetString();

        protected override string GetTitle(GameObject source) => $"Is Playing {this.m_AudioID}";

        protected override bool Run(Args args)
        {
            string audioID = this.m_AudioID.Get(args);
            return AudioManager.IsPlaying(new Guid(audioID));
        }
    }
}