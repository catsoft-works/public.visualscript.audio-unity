using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Get, Set]

    [Title("Master Volume")]
    [Category("Audio/Volume/Master")]

    [Description("The current volume of the Master channel")]
    [Image(typeof(IconCrown), ColorTheme.Type.Yellow, typeof(BadgeVolume))]

    [Keywords("Noise", "Level")]

    [Serializable]
    public class TypeNumberAudioUnityVolumeMaster : TypeNumber
    {
        public override double GetPreview(GameObject source) => 1f;

        public override double Get(Args args)
        {
            return VolumeManager.Master.Value;
        }

        public override void Set(double value, Args args)
        {
            VolumeManager.Master.Value = (float) value;
        }

        public override string ToString()
        {
            return "Master Volume";
        }

        protected override SubscriptionMode Subscription => SubscriptionMode.Custom;

        protected override Guid OnSubscribe(Action<Args> callback, Args args)
        {
            return VolumeManager.Master.Subscribe(callback, args);
        }

        protected override void OnUnsubscribe(Guid subscriptionId)
        {
            VolumeManager.Master.Unsubscribe(subscriptionId);
        }
    }
}