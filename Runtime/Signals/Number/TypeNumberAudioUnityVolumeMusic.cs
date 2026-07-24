using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Get, Set]

    [Title("Music Volume")]
    [Category("Audio/Volume/Music")]

    [Description("The current volume of the Music channel")]
    [Image(typeof(IconHeadset), ColorTheme.Type.Yellow, typeof(BadgeVolume))]

    [Keywords("Noise", "Level")]

    [Serializable]
    public class TypeNumberAudioUnityVolumeMusic : TypeNumber
    {
        public override double GetPreview(GameObject source) => 1f;

        public override double Get(Args args)
        {
            return VolumeManager.Music.Value;
        }

        public override void Set(double value, Args args)
        {
            VolumeManager.Music.Value = (float) value;
        }

        public override string ToString()
        {
            return "Music Volume";
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.Custom;

        protected override Guid OnSubscribe(Action<Args> callback, Args args)
        {
            return VolumeManager.Music.Subscribe(callback, args);
        }

        protected override void OnUnsubscribe(Guid subscriptionId)
        {
            VolumeManager.Music.Unsubscribe(subscriptionId);
        }
    }
}