using System;
using UnityEngine;

namespace VisualScript.Runtime.AudioUnity
{
    [Serializable]
    public class AudioData
    {
        private enum LoopMode
        {
            None,
            Loop
        }

        private enum LocationMode
        {
            None = 0,
            Target = 1,
            Position = 2,
            Transform = 3
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private LoopMode m_Loop;
        [SerializeField] private GetNumber m_Transition;

        [SerializeField] private TimeScale m_TimeScale;
        [SerializeField] private GetInteger m_Priority;
        [SerializeField] private GetNumber m_Volume;
        [SerializeField] private GetNumber m_Pitch;
        [SerializeField] private GetNumber m_StereoPan;
        [SerializeField] private GetNumber m_SpatialBlend;
        [SerializeField] private GetNumber m_ReverbZoneMix;

        [SerializeField] private RolloffMode m_VolumeRolloff;
        [SerializeField] private GetNumber m_MinDistance;
        [SerializeField] private GetNumber m_MaxDistance;
        [SerializeField] private GetNumber m_DopplerLevel;
        [SerializeField] private GetNumber m_Spread;

        [SerializeField] private EnablerSignalGetGameObject m_HasTarget;

        [SerializeField] private LocationMode m_Location;
        [SerializeField] private GetVector3 m_Position;
        [SerializeField] private GetGameObject m_Transform;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public AudioData(
            bool loop,
            float transition,
            TimeMode timeMode,
            bool randomPitch,
            float spatialBlend,
            bool hasTarget)
        {
            this.m_Loop = loop ? LoopMode.Loop : LoopMode.None;
            this.m_Transition = new GetNumber(transition);
            this.m_TimeScale = new TimeScale(timeMode);
            this.m_Priority = new GetInteger(128);
            this.m_Volume = new GetNumber(1f);
            this.m_Pitch = randomPitch
                ? new GetNumber(new TypeNumberRandomRange(0.95f, 1.05f))
                : new GetNumber(1f);
            this.m_StereoPan = new GetNumber(0f);
            this.m_SpatialBlend = new GetNumber(spatialBlend);
            this.m_ReverbZoneMix = new GetNumber(1f);
            this.m_VolumeRolloff = RolloffMode.Logarithmic;
            this.m_MinDistance = new GetNumber(1f);
            this.m_MaxDistance = new GetNumber(500f);
            this.m_DopplerLevel = new GetNumber(0f);
            this.m_Spread = new GetNumber(0f);
            this.m_HasTarget = new EnablerSignalGetGameObject(hasTarget, new GetGameObject());
            this.m_Location = hasTarget 
                ? LocationMode.Target
                : spatialBlend >= 0.95f
                    ? LocationMode.Position
                    : LocationMode.None;
            this.m_Position = new GetVector3();
            this.m_Transform = new GetGameObject();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public AudioConfig Get(AudioChannel channel, Args args)
        {
            GameObject target = this.m_HasTarget.IsEnabled
                ? this.m_HasTarget.Get(args)
                : null;

            return new AudioConfig(
                this.m_TimeScale.GetTimeScale(args),
                this.m_Transition.GetFloat(args),
                channel,
                this.m_Loop switch
                {
                    LoopMode.None => false,
                    LoopMode.Loop => true,
                    _ => throw new ArgumentOutOfRangeException()
                },
                this.m_Priority.GetInt(args),
                this.m_Volume.GetFloat(args),
                this.m_Pitch.GetFloat(args),
                this.m_StereoPan.GetFloat(args),
                this.m_SpatialBlend.GetFloat(args),
                this.m_ReverbZoneMix.GetFloat(args),
                (AudioRolloffMode) this.m_VolumeRolloff,
                this.m_MinDistance.GetFloat(args),
                this.m_MaxDistance.GetFloat(args),
                this.m_DopplerLevel.GetFloat(args),
                this.m_Spread.GetFloat(args),
                this.m_HasTarget.IsEnabled, target,
                this.m_Location switch
                {
                    LocationMode.None => null,
                    LocationMode.Target => target != null ? target.transform : null,
                    LocationMode.Position => null,
                    LocationMode.Transform => this.m_Transform.Get<Transform>(args),
                    _ => throw new ArgumentOutOfRangeException()
                },
                this.m_Location switch
                {
                    LocationMode.None => default,
                    LocationMode.Target => default,
                    LocationMode.Position => this.m_Position.Get(args),
                    LocationMode.Transform => default,
                    _ => throw new ArgumentOutOfRangeException()
                }
            );
        }
    }
}