using System;
using UnityEngine;

namespace VisualScript.Runtime.AudioUnity
{
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Visual Script/Audio/Audio Source Volume")]

    [Icon("Packages/works.catsoft.visualscript-audio-unity/Editor/Gizmos/GizmoAudioSourceVolume.png")]

    [Serializable]
    public class AudioSourceVolume : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private AudioChannel m_Channel = AudioChannel.Ambient;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private AudioSource m_AudioSource;

        // INITIALIZERS: --------------------------------------------------------------------------

        private void Awake()
        {
            this.m_AudioSource = this.GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            VolumeManager.EventChange += this.RefreshVolume;
            this.RefreshVolume();
        }

        private void OnDisable()
        {
            VolumeManager.EventChange -= this.RefreshVolume;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RefreshVolume()
        {
            float master = VolumeManager.Master.Value;
            this.m_AudioSource.volume = VolumeManager.Get(this.m_Channel) * master;
        }
    }
}