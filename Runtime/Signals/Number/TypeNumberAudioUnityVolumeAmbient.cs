using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Get, Set]

    [Title("Ambient Volume")]
    [Category("Audio/Volume/Ambient")]

    [Description("The current volume of the Ambient channel")]
    [Image(typeof(IconBird), ColorTheme.Type.Yellow, typeof(BadgeVolume))]

    [Keywords("Noise", "Level")]

    [Serializable]
    public class TypeNumberAudioUnityVolumeAmbient : TypeNumber
    {
        public override double GetPreview(GameObject source) => 1f;

        public override double Get(Args args)
        {
            return VolumeManager.Ambient.Value;
        }

        public override void Set(double value, Args args)
        {
            VolumeManager.Ambient.Value = (float) value;
        }

        public override string ToString()
        {
            return "Ambient Volume";
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.Custom;

        protected override Guid OnSubscribe(Action<Args> callback, Args args)
        {
            return VolumeManager.Ambient.Subscribe(callback, args);
        }

        protected override void OnUnsubscribe(Guid subscriptionId)
        {
            VolumeManager.Ambient.Unsubscribe(subscriptionId);
        }
    }
}