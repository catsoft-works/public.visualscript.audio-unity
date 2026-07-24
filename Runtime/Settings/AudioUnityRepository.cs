using System;
using UnityEngine;
using UnityEngine.Audio;

namespace VisualScript.Runtime.AudioUnity
{
    [Serializable] [Repository(ID)]
    public class AudioUnityRepository : TRepository<AudioUnityRepository>
    {
        private const string ID = "vs.audio-unity";

        // REPOSITORY PROPERTIES: -----------------------------------------------------------------

        public override string Id => ID;

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private AudioMixerGroup m_SoundEffects;
        [SerializeField] private AudioMixerGroup m_Ambient;
        [SerializeField] private AudioMixerGroup m_Music;
        [SerializeField] private AudioMixerGroup m_Voice;
        [SerializeField] private AudioMixerGroup m_UI;

        // PROPERTIES: ----------------------------------------------------------------------------

        public AudioMixerGroup SoundEffects => this.m_SoundEffects;
        public AudioMixerGroup Ambient => this.m_Ambient;
        public AudioMixerGroup Music => this.m_Music;
        public AudioMixerGroup Voice => this.m_Voice;
        public AudioMixerGroup UI => this.m_UI;

        // EDITOR METHODS: ------------------------------------------------------------------------

        #if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        public static void InitializeOnEnterPlayMode() => Dispose();

        #endif
    }
}