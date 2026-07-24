using System;
using UnityEngine;
using UnityEngine.Audio;

namespace VisualScript.Runtime.AudioUnity
{
    internal class Audio : IAudio
    {
        private const float TIME_CONSTANT = 5f;
        private const float DEFAULT_DECAY = 16f;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private readonly AudioSource m_AudioSource;
        [NonSerialized] private AudioConfig m_Config;

        [NonSerialized] private float m_Volume;
        [NonSerialized] private float m_Pitch;

        [NonSerialized] private float m_VolumeTransitionCurrent;
        [NonSerialized] private float m_VolumeTransitionTarget;
        [NonSerialized] private float m_VolumeTransitionDecay;

        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] public Guid AudioId { get; private set; }

        public bool IsPlaying => this.m_AudioSource.isPlaying;

        public AudioResource Resource => this.m_AudioSource.resource;
        public AudioChannel Channel => this.m_Config.Channel;

        public bool HasTarget => this.m_Config.HasTarget;
        public GameObject Target => this.m_Config.HasTarget ? this.m_Config.Target : null;

        int IAudio.Priority
        {
            get => this.m_AudioSource.priority;
            set => this.m_AudioSource.priority = value;
        }

        float IAudio.Volume
        {
            get => this.m_Volume;
            set => this.m_Volume = value;
        }

        float IAudio.Pitch
        {
            get => this.m_Pitch;
            set => this.m_Pitch = value;
        }

        float IAudio.StereoPan
        {
            get => this.m_AudioSource.panStereo;
            set => this.m_AudioSource.panStereo = value;
        }

        float IAudio.SpatialBlend
        {
            get => this.m_AudioSource.spatialBlend;
            set => this.m_AudioSource.spatialBlend = value;
        }

        float IAudio.ReverbZoneMix
        {
            get => this.m_AudioSource.reverbZoneMix;
            set => this.m_AudioSource.reverbZoneMix = value;
        }

        RolloffMode IAudio.VolumeRolloff
        {
            get => (RolloffMode) this.m_AudioSource.rolloffMode;
            set => this.m_AudioSource.rolloffMode = (AudioRolloffMode) value;
        }

        float IAudio.MinDistance
        {
            get => this.m_AudioSource.minDistance;
            set => this.m_AudioSource.minDistance = value;
        }

        float IAudio.MaxDistance
        {
            get => this.m_AudioSource.maxDistance;
            set => this.m_AudioSource.maxDistance = value;
        }

        float IAudio.DopplerLevel
        {
            get => this.m_AudioSource.dopplerLevel;
            set => this.m_AudioSource.dopplerLevel = value;
        }

        float IAudio.Spread
        {
            get => this.m_AudioSource.spread;
            set => this.m_AudioSource.spread = value;
        }

        // INITIALIZERS: --------------------------------------------------------------------------

        public Audio()
        {
            int index = AudioManager.Transform.childCount;
            GameObject gameObject = new GameObject($"AudioSource_{index}", typeof(AudioSource));

            gameObject.transform.SetParent(AudioManager.Transform);

            this.m_AudioSource = gameObject.GetComponent<AudioSource>();
            gameObject.SetActive(false);
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public Guid Play(Guid audioId, AudioResource resource, AudioConfig config)
        {
            this.AudioId = audioId;

            this.m_AudioSource.resource = resource;
            this.m_Config = config;

            bool hasTransition = this.m_Config.Transition > float.Epsilon;

            this.m_VolumeTransitionCurrent = hasTransition ? 0f : 1f;
            this.m_VolumeTransitionTarget = 1f;
            this.m_VolumeTransitionDecay = hasTransition
                ? TIME_CONSTANT / this.m_Config.Transition
                : DEFAULT_DECAY;

            this.m_Volume = this.m_Config.Volume;
            this.m_AudioSource.volume = hasTransition ? 0f : this.m_Volume;

            this.m_Pitch = this.m_Config.Pitch;
            this.m_AudioSource.pitch = this.m_Pitch;

            this.m_AudioSource.outputAudioMixerGroup = this.m_Config.Channel switch
            {
                AudioChannel.SoundEffect => AudioUnityRepository.Get.SoundEffects,
                AudioChannel.Ambient => AudioUnityRepository.Get.Ambient,
                AudioChannel.Music => AudioUnityRepository.Get.Music,
                AudioChannel.Voice => AudioUnityRepository.Get.Voice,
                AudioChannel.UI => AudioUnityRepository.Get.UI,
                _ => throw new ArgumentOutOfRangeException()
            };

            this.m_AudioSource.loop = this.m_Config.Loop;
            this.m_AudioSource.priority = this.m_Config.Priority;
            this.m_AudioSource.panStereo = this.m_Config.StereoPan;
            this.m_AudioSource.spatialBlend = this.m_Config.SpatialBlend;
            this.m_AudioSource.reverbZoneMix = this.m_Config.ReverbZoneMix;
            this.m_AudioSource.rolloffMode = this.m_Config.VolumeRolloff;
            this.m_AudioSource.minDistance = this.m_Config.MinDistance;
            this.m_AudioSource.maxDistance = this.m_Config.MaxDistance;
            this.m_AudioSource.dopplerLevel = this.m_Config.DopplerLevel;
            this.m_AudioSource.spread = this.m_Config.Spread;

            this.m_AudioSource.transform.localPosition = this.m_Config.LocationTransform != null
                ? this.m_Config.LocationTransform.position
                : this.m_Config.LocationPosition;

            this.m_AudioSource.gameObject.SetActive(true);
            this.m_AudioSource.Play();

            return audioId;
        }

        public void Stop()
        {
            this.m_AudioSource.Stop();
        }

        public async Awaitable Stop(float transition)
        {
            if (transition <= float.Epsilon)
            {
                this.m_AudioSource.Stop();
                return;
            }

            this.m_VolumeTransitionTarget = 0f;
            this.m_VolumeTransitionDecay = TIME_CONSTANT / transition;

            this.m_AudioSource.SetScheduledEndTime(AudioSettings.dspTime + transition);
            while (!AppManager.IsExiting && this.m_AudioSource.isPlaying)
            {
                await Awaitable.NextFrameAsync();
            }
        }

        // UPDATE METHOD: -------------------------------------------------------------------------

        public bool OnUpdate()
        {
            this.m_VolumeTransitionCurrent = MathUtils.ExponentialDecay(
                this.m_VolumeTransitionCurrent,
                this.m_VolumeTransitionTarget,
                this.m_VolumeTransitionDecay,
                TimeManager.GetDeltaTime(this.m_Config.TimeScale)
            );

            float volume = VolumeManager.Get(this.m_Config.Channel) * VolumeManager.Master.Value;
            this.m_AudioSource.volume = this.m_VolumeTransitionCurrent * this.m_Volume * volume;

            float timeScale = TimeManager.GetTimeScale(this.m_Config.TimeScale);
            this.m_AudioSource.pitch = this.m_Pitch * timeScale;

            this.m_AudioSource.transform.localPosition = this.m_Config.LocationTransform != null
                ? this.m_Config.LocationTransform.position
                : this.m_Config.LocationPosition;

            if (this.IsPlaying) return false;

            this.m_AudioSource.gameObject.SetActive(false);
            return true;
        }
    }
}