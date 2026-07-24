using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Get, Set]

    [Title("Voice Volume")]
    [Category("Audio/Volume/Voice")]

    [Description("The current volume of the Voice channel")]
    [Image(typeof(IconFace), ColorTheme.Type.Yellow, typeof(BadgeVolume))]

    [Keywords("Noise", "Level")]

    [Serializable]
    public class TypeNumberAudioUnityVolumeVoice : TypeNumber
    {
        public override double GetPreview(GameObject source) => 1f;

        public override double Get(Args args)
        {
            return VolumeManager.Voice.Value;
        }

        public override void Set(double value, Args args)
        {
            VolumeManager.Voice.Value = (float) value;
        }

        public override string ToString()
        {
            return "Voice Volume";
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.Custom;

        protected override Guid OnSubscribe(Action<Args> callback, Args args)
        {
            return VolumeManager.Voice.Subscribe(callback, args);
        }

        protected override void OnUnsubscribe(Guid subscriptionId)
        {
            VolumeManager.Voice.Unsubscribe(subscriptionId);
        }
    }
}