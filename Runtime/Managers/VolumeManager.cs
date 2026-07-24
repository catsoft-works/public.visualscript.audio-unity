using System;
using UnityEngine;

namespace VisualScript.Runtime.AudioUnity
{
    [AddComponentMenu("")]
    public class VolumeManager : Singleton<VolumeManager>, ISaveLoad
    {
        private static readonly IdString ID = new IdString("unity-audio-volume-info");

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private readonly Volumes m_Volumes = new Volumes();

        // PROPERTIES: ----------------------------------------------------------------------------

        public static Volume Master => Instance.m_Volumes.Master;
        public static Volume SoundEffect => Instance.m_Volumes.SoundEffect;
        public static Volume Ambient => Instance.m_Volumes.Ambient;
        public static Volume Music => Instance.m_Volumes.Music;
        public static Volume Voice => Instance.m_Volumes.Voice;
        public static Volume UI => Instance.m_Volumes.UI;

        // EVENTS: --------------------------------------------------------------------------------

        public static event Action EventChange;

        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnCreate()
        {
            base.OnCreate();
            SaveLoadManager.Subscribe(this);

            this.m_Volumes.Master.EventChange += OnChange;
            this.m_Volumes.SoundEffect.EventChange += OnChange;
            this.m_Volumes.Ambient.EventChange += OnChange;
            this.m_Volumes.Music.EventChange += OnChange;
            this.m_Volumes.Voice.EventChange += OnChange;
            this.m_Volumes.UI.EventChange += OnChange;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static float Get(AudioChannel channel)
        {
            if (AppManager.IsExiting) return 0f;

            return channel switch
            {
                AudioChannel.SoundEffect => Instance.m_Volumes.SoundEffect.Value,
                AudioChannel.Ambient => Instance.m_Volumes.Ambient.Value,
                AudioChannel.Music => Instance.m_Volumes.Music.Value,
                AudioChannel.Voice => Instance.m_Volumes.Voice.Value,
                AudioChannel.UI => Instance.m_Volumes.UI.Value,
                _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
            };
        }

        public static void Set(AudioChannel channel, float volume)
        {
            if (AppManager.IsExiting) return;

            switch (channel)
            {
                case AudioChannel.SoundEffect: Instance.m_Volumes.SoundEffect.Value = volume; break;
                case AudioChannel.Ambient: Instance.m_Volumes.Ambient.Value = volume; break;
                case AudioChannel.Music: Instance.m_Volumes.Music.Value = volume; break;
                case AudioChannel.Voice: Instance.m_Volumes.Voice.Value = volume; break;
                case AudioChannel.UI: Instance.m_Volumes.Music.Value = volume; break;
                default: throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
        }

        // SUBSCRIPTIONS: -------------------------------------------------------------------------

        public static Guid Subscribe(AudioChannel channel, Action<Args> callback, Args args)
        {
            if (AppManager.IsExiting) return Guid.Empty;

            return channel switch
            {
                AudioChannel.SoundEffect => SoundEffect.Subscribe(callback, args),
                AudioChannel.Ambient => Ambient.Subscribe(callback, args),
                AudioChannel.Music => Music.Subscribe(callback, args),
                AudioChannel.Voice => Voice.Subscribe(callback, args),
                AudioChannel.UI => UI.Subscribe(callback, args),
                _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
            };
        }

        public static void Unsubscribe(AudioChannel channel, Guid subscriptionId)
        {
            if (AppManager.IsExiting) return;

            switch (channel)
            {
                case AudioChannel.SoundEffect: SoundEffect.Unsubscribe(subscriptionId); break;
                case AudioChannel.Ambient: Ambient.Unsubscribe(subscriptionId); break;
                case AudioChannel.Music: Music.Unsubscribe(subscriptionId); break;
                case AudioChannel.Voice: Voice.Unsubscribe(subscriptionId); break;
                case AudioChannel.UI: UI.Unsubscribe(subscriptionId); break;
                default: throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void OnChange(float _)
        {
            EventChange?.Invoke();
        }

        // SAVE LOAD: -----------------------------------------------------------------------------

        IdString ISaveLoad.Id => ID;

        Type ISaveLoad.Type => typeof(Volumes);

        bool ISaveLoad.IsShared => true;

        bool ISaveLoad.IsPersistent => true;

        void ISaveLoad.OnReset()
        {
            this.m_Volumes.Reset();
        }

        object ISaveLoad.OnSave()
        {
            return this.m_Volumes.OnSave();
        }

        Awaitable ISaveLoad.OnLoad(object value)
        {
            this.m_Volumes.OnLoad(value);
            return AwaitableUtils.DontAwait;
        }
    }
}