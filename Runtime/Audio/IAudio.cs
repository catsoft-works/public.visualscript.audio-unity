using UnityEngine;
using UnityEngine.Audio;

namespace VisualScript.Runtime.AudioUnity
{
    public interface IAudio
    {
        AudioResource Resource { get; }
        AudioChannel Channel { get; }

        bool HasTarget { get; }
        GameObject Target { get; }

        int Priority { get; set; }
        float Volume { get; set; }
        float Pitch { get; set; }
        float StereoPan { get; set; }
        float SpatialBlend { get; set; }
        float ReverbZoneMix { get; set; }

        RolloffMode VolumeRolloff { get; set; }
        float MinDistance { get; set; }
        float MaxDistance { get; set; }
        float DopplerLevel { get; set; }
        float Spread { get; set; }
    }
}