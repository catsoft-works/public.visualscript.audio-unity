using System;
using UnityEngine;

namespace VisualScript.Runtime.AudioUnity
{
    public readonly struct AudioConfig
    {
        [NonSerialized] public readonly ITimeScale TimeScale;
        [NonSerialized] public readonly float Transition;
        [NonSerialized] public readonly AudioChannel Channel;
        [NonSerialized] public readonly bool Loop;

        [NonSerialized] public readonly int Priority;
        [NonSerialized] public readonly float Volume;
        [NonSerialized] public readonly float Pitch;
        [NonSerialized] public readonly float StereoPan;
        [NonSerialized] public readonly float SpatialBlend;
        [NonSerialized] public readonly float ReverbZoneMix;

        [NonSerialized] public readonly AudioRolloffMode VolumeRolloff;
        [NonSerialized] public readonly float MinDistance;
        [NonSerialized] public readonly float MaxDistance;
        [NonSerialized] public readonly float DopplerLevel;
        [NonSerialized] public readonly float Spread;

        [NonSerialized] public readonly bool HasTarget;
        [NonSerialized] public readonly GameObject Target;

        [NonSerialized] public readonly Transform LocationTransform;
        [NonSerialized] public readonly Vector3 LocationPosition;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public AudioConfig(
            ITimeScale timeScale,
            float transition,
            AudioChannel channel,
            bool loop,
            int priority,
            float volume,
            float pitch,
            float stereoPan,
            float spatialBlend,
            float reverbZoneMix,
            AudioRolloffMode volumeRolloff,
            float minDistance,
            float maxDistance,
            float dopplerLevel,
            float spread,
            bool hasTarget,
            GameObject target = null,
            Transform locationTransform = null,
            Vector3 locationPosition = default)
        {
            this.TimeScale = timeScale;
            this.Transition = transition;
            this.Channel = channel;
            this.Loop = loop;
            this.Priority = priority;
            this.Volume = volume;
            this.Pitch = pitch;
            this.StereoPan = stereoPan;
            this.SpatialBlend = spatialBlend;
            this.ReverbZoneMix = reverbZoneMix;
            this.VolumeRolloff = volumeRolloff;
            this.MinDistance = minDistance;
            this.MaxDistance = maxDistance;
            this.DopplerLevel = dopplerLevel;
            this.Spread = spread;
            this.HasTarget = hasTarget;
            this.Target = target;
            this.LocationTransform = locationTransform;
            this.LocationPosition = locationPosition;
        }
    }
}