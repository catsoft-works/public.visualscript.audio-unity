using System;
using UnityEngine;
using UnityEngine.Audio;

namespace VisualScript.Runtime.AudioUnity
{
    [Parameter("Loop", "If the audio plays just once for loops forever")]
    [Parameter("Transition", "The fade in effect in seconds")]

    [Parameter("Time Mode", "Whether this audio is affected by the time scale or not")]
    [Parameter("Priority", "The priority of this audio versus the other sounds")]
    [Parameter("Volume", "The volume for this audio")]
    [Parameter("Pitch", "The pitch value for this audio")]
    [Parameter("Stereo Pan", "The mix of the left and right channels")]
    [Parameter("Spatial Blend", "The mix between 2D and 3D audio")]
    [Parameter("Reverb Zone Mix", "How much the audio is affected by the reverb zone")]

    [Parameter("Volume Rolloff", "How the audio volume is affected by distance")]
    [Parameter("Min Distance", "The minimum distance for the audio to be heard")]
    [Parameter("Max Distance", "The maximum distance for the audio to be heard")]
    [Parameter("Doppler Level", "The amount of doppler effect for the audio")]
    [Parameter("Spread", "The spread of this 3D audio in speaker space")]

    [Parameter("Target", "Whether the audio is associated with a particular Game Object or not")]
    [Parameter("Location", "The point in space where this audio is played")]

    [Serializable]
    public abstract class InstructionAudioUnityPlayBase : Instruction
    {
        [SerializeField] private GetAudioResource m_Audio;
        [SerializeField] private AudioData m_Data;

        protected override string GetTitle(GameObject source)
        {
            string channel = TextUtils.Humanize(this.Channel);
            return $"Play {this.m_Audio} as {channel}";
        }

        protected abstract AudioChannel Channel { get; }

        protected InstructionAudioUnityPlayBase()
        {
            this.m_Audio = new GetAudioResource();
            this.m_Data = this.CreateDefaultAudioData();
        }

        protected override Awaitable Run(Args args)
        {
            AudioResource audio = this.m_Audio.Get(args);
            AudioManager.Play(audio, this.m_Data.Get(this.Channel, args));

            return Done;
        }

        protected abstract AudioData CreateDefaultAudioData();
    }
}