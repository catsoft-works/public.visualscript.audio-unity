using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Get, Set]

    [Title("UI Volume")]
    [Category("Audio/Volume/UI")]

    [Description("The current volume of the UI channel")]
    [Image(typeof(IconButton), ColorTheme.Type.Yellow, typeof(BadgeVolume))]

    [Keywords("Noise", "Level")]

    [Serializable]
    public class TypeNumberAudioUnityVolumeUI : TypeNumber
    {
        public override double GetPreview(GameObject source) => 1f;

        public override double Get(Args args)
        {
            return VolumeManager.UI.Value;
        }

        public override void Set(double value, Args args)
        {
            VolumeManager.UI.Value = (float) value;
        }

        public override string ToString()
        {
            return "UI Volume";
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.Custom;

        protected override Guid OnSubscribe(Action<Args> callback, Args args)
        {
            return VolumeManager.UI.Subscribe(callback, args);
        }

        protected override void OnUnsubscribe(Guid subscriptionId)
        {
            VolumeManager.UI.Unsubscribe(subscriptionId);
        }
    }
}