using System;
using UnityEngine;
using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    [Title("Is Playing on Game Object")]
    [Image(typeof(IconMusic), ColorTheme.Type.Yellow, typeof(BadgeCubeSolid))]

    [Description("Returns true if any Audio queue is currently being played on a Game Object")]
    [Category("Audio/Is Playing on Game Object")]

    [Parameter("Game Object", "The targeted Game Object to check")]

    [Keywords("Current", "Now")]

    [Serializable]
    public class ConditionAudioUnityIsPlayingGameObject : Condition
    {
        [SerializeField] private GetGameObject m_GameObject = new GetGameObject();

        protected override string GetTitle(GameObject source) => $"Is Playing on {this.m_GameObject}";

        protected override bool Run(Args args)
        {
            GameObject gameObject = this.m_GameObject.Get(args);
            return AudioManager.IsPlaying(gameObject);
        }
    }
}