using System;
using UnityEngine;
using UnityEngine.Audio;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Is Playing Audio Resource")]
    [Image(typeof(IconMusic), ColorTheme.Type.Yellow)]

    [Description("Returns true if an Audio Resource is currently being played")]
    [Category("Audio/Is Playing Audio Resource")]

    [Parameter("Audio Resource", "The Audio Resource to check")]

    [Keywords("Current", "Now")]

    [Serializable]
    public class ConditionAudioUnityIsPlayingAudioResource : Condition
    {
        [SerializeField] private GetAudioResource m_AudioResource = new GetAudioResource();

        protected override string GetTitle(GameObject source) => $"Is Playing {this.m_AudioResource}";

        protected override bool Run(Args args)
        {
            AudioResource audioResource = this.m_AudioResource.Get(args);
            return AudioManager.IsPlaying(audioResource);
        }
    }
}