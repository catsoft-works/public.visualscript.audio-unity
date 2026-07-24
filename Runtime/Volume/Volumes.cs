using System;
using UnityEngine;

namespace VisualScript.Runtime.AudioUnity
{
    [Serializable]
    public class Volumes
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private Volume m_Master;
        [SerializeField] private Volume m_SoundEffect;
        [SerializeField] private Volume m_Ambient;
        [SerializeField] private Volume m_Music;
        [SerializeField] private Volume m_Voice;
        [SerializeField] private Volume m_UI;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Volume Master => this.m_Master;
        public Volume SoundEffect => this.m_SoundEffect;
        public Volume Ambient => this.m_Ambient;
        public Volume Music => this.m_Music;
        public Volume Voice => this.m_Voice;
        public Volume UI => this.m_UI;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public Volumes()
        {
            this.m_Master = new Volume();
            this.m_SoundEffect = new Volume();
            this.m_Ambient = new Volume();
            this.m_Music = new Volume();
            this.m_Voice = new Volume();
            this.m_UI = new Volume();
        }

        public Volumes(
            float master,
            float soundEffect,
            float ambient,
            float music,
            float voice,
            float ui)
        {
            this.m_Master = new Volume(master);
            this.m_SoundEffect = new Volume(soundEffect);
            this.m_Ambient = new Volume(ambient);
            this.m_Music = new Volume(music);
            this.m_Voice = new Volume(voice);
            this.m_UI = new Volume(ui);
        }

        // INTERNAL METHODS: ----------------------------------------------------------------------

        internal void Reset()
        {
            this.m_Master.Value = 1f;
            this.m_SoundEffect.Value = 1f;
            this.m_Ambient.Value = 1f;
            this.m_Music.Value = 1f;
            this.m_Voice.Value = 1f;
            this.m_UI.Value = 1f;
        }

        internal object OnSave()
        {
            return new Volumes(
                this.m_Master.Value,
                this.m_SoundEffect.Value,
                this.m_Ambient.Value,
                this.m_Music.Value,
                this.m_Voice.Value,
                this.m_UI.Value
            );
        }

        internal void OnLoad(object value)
        {
            if (value is not Volumes volumes) return;

            this.m_Master.Value = volumes.m_Master.Value;
            this.m_SoundEffect.Value = volumes.m_SoundEffect.Value;
            this.m_Ambient.Value = volumes.m_Ambient.Value;
            this.m_Music.Value = volumes.m_Music.Value;
            this.m_Voice.Value = volumes.m_Voice.Value;
            this.m_UI.Value = volumes.m_UI.Value;
        }
    }
}