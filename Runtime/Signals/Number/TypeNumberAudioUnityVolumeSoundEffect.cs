using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Get, Set]

    [Title("Sound Effect Volume")]
    [Category("Audio/Volume/Sound Effect")]

    [Description("The current volume of the Sound Effect channel")]
    [Image(typeof(IconMusicNote), ColorTheme.Type.Yellow, typeof(BadgeVolume))]

    [Keywords("Noise", "Level")]

    [Serializable]
    public class TypeNumberAudioUnityVolumeSoundEffect : TypeNumber
    {
        public override double GetPreview(GameObject source) => 1f;

        public override double Get(Args args)
        {
            return VolumeManager.SoundEffect.Value;
        }

        public override void Set(double value, Args args)
        {
            VolumeManager.SoundEffect.Value = (float) value;
        }

        public override string ToString()
        {
            return "Sound Effect Volume";
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.Custom;

        protected override Guid OnSubscribe(Action<Args> callback, Args args)
        {
            return VolumeManager.SoundEffect.Subscribe(callback, args);
        }

        protected override void OnUnsubscribe(Guid subscriptionId)
        {
            VolumeManager.SoundEffect.Unsubscribe(subscriptionId);
        }
    }
}